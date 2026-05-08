using king.data;
using king.Models.players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace king.Controllers.players
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        public readonly ILogger<PlayersController> _logger;
        private readonly IPlayerService _playerService;
        private readonly AppDbContext _dbContext;
        public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService, AppDbContext dbContext)
        {
            _logger = logger;
            _playerService = playerService;
            _dbContext = dbContext;
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

      
    }
}
