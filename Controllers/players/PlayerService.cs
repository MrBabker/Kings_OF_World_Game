using FirebaseAdmin.Auth;
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

       
    }
}
