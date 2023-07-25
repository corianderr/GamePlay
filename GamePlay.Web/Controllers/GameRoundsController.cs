using GamePlay.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;

public class GameRoundsController : Controller
{
    private readonly IGameRoundService _gameRoundService;

    public GameRoundsController(IGameRoundService gameRoundService)
    {
        _gameRoundService = gameRoundService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var rounds = await _gameRoundService.GetAllAsync();
        return View(rounds.ToList());
    }
    
    // GET: GameRounds/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var round = await _gameRoundService.GetByIdAsync(id);
        return View(round);
    }
}