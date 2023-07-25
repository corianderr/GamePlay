using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models.GameRound;
using GamePlay.Web.Models;
using Microsoft.AspNet.Identity;
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
    
    // GET: GameRounds/Create
    public async Task<ActionResult> Create(Guid gameId)
    {
        var createViewModel = new CreateGameRoundViewModel()
        {
            PreviousPlaces = await _gameRoundService.GetDistinctPlacesAsync(User.Identity.GetUserId()),
        };
        return View();
    }

    // POST: GameRounds/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateGameRoundViewModel createViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return View(createViewModel);

            await _gameRoundService.AddAsync(createViewModel.GameRound);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(createViewModel);
        }
    }


}