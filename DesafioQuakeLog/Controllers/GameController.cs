using Microsoft.AspNetCore.Mvc;
using DesafioQuakeLog.Models;
using DesafioQuakeLog.Services;

namespace DesafioQuakeLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetGames()
        {
            try
            {
                var games = _gameService.GetAllGames();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{name}")]
        public ActionResult<Game> GetName(string name)
        {
            try
            {
                var game = _gameService.GetGameName(name);

                if (game == null)
                {
                    return NotFound($"Game with name '{name}' not found.");
                }

                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
