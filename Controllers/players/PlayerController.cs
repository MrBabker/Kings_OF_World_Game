using FirebaseAdmin.Auth;
using king.data;
using king.Models.players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Text;

namespace king.Controllers.players
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        public readonly ILogger<PlayersController> _logger;
        private readonly IPlayerService _playerService;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService, AppDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            _playerService = playerService;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet("all/{page}/{pageSize}")]
        public async Task<IActionResult> GetAllPlayers(int page, int pageSize)
        {
            var players = await _playerService.GetAllPlayers(page, 10);
            if (players == null || players.Length == 0)
            {
                return NotFound("No players found.");
            }
            return Ok(players);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO req)
        {
            // 1. إنشاء مستخدم في Firebase
            var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
            {
                Email = req.Email,
                Password = req.Password
            });

            // 2. إرسال تحقق الإيميل
            var link = await FirebaseAuth.DefaultInstance
                .GenerateEmailVerificationLinkAsync(req.Email);

            // (هنا ترسل الإيميل بأي خدمة SMTP لاحقاً)

            // 3. تخزين في PostgreSQL
            var player = new PlayerModel
            {
                FirebaseId = user.Uid,
                Email = req.Email
            };

            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();

            return Ok("User created, verify email");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO req)
        {
            using var client = new HttpClient();

            var apiKey = _configuration["Firebase:WebApiKey"];

            var content = new StringContent($@"
    {{
        ""email"": ""{req.Email}"",
        ""password"": ""{req.Password}"",
        ""returnSecureToken"": true
    }}", Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}",
                content);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return Unauthorized(result);

            return Ok(result);
        }


    }
}
