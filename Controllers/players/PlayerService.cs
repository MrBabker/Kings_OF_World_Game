using Google.Apis.Auth;
using king.data;
using king.Models.players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace king.Controllers.players
{
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public PlayerService(AppDbContext appDbContext, IConfiguration configuration) 
        { 
            _appDbContext = appDbContext;
            _configuration = configuration;
        }   

        public async Task<PlayerSelectsDTO[]> GetAllPlayers(int page , int pageSize)
        {
            return await _appDbContext.Players
        .Select(p => new PlayerSelectsDTO
        {
            Id = p.Id,
            Name = p.Name,
            Username = p.Username,
            Email = p.Email,
            Level = p.Level
        })
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToArrayAsync();
        }

        public async Task<string> GoogleAuth(GoogleLoginRequest request)
        {


            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

            var email = payload.Email;

            var user = await _appDbContext.Players
                .FirstOrDefaultAsync(u => u.Email == email);

            // إنشاء حساب جديد
            if (user == null)
            {
                user = new PlayerModel
                {
                    //Email = payload.Email,
                    //Name = payload.Name,
                    //Username = payload.Email.Split('@')[0],
                    //IsGoogleAccount = true,
                    //Level = 0,
                    Email = "s@s.com",
                    Name = "nameo",
                    Username = "@namo",
                    IsGoogleAccount = true,
                    Level = 0,
                };

                _appDbContext.Players.Add(user);
                await _appDbContext.SaveChangesAsync();
            }

            // إنشاء JWT
            var token = GenerateJwt(user);

            return token;
        }


        public string GenerateJwt(PlayerModel user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Name, user.Name)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
