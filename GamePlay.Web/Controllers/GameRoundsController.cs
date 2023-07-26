using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models.GameRound;
using GamePlay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;

public class GameRoundsController : Controller
{
    private readonly IGameRoundService _gameRoundService;
    private readonly IGameService _gameService;
    private readonly IUserService _userService;

    public GameRoundsController(IGameRoundService gameRoundService, IGameService gameService, IUserService userService)
    {
        _gameRoundService = gameRoundService;
        _gameService = gameService;
        _userService = userService;
    }
    // GET
    public async Task<IActionResult> Index(Guid? gameId = null)
    {
        IEnumerable<GameRoundModel> rounds;
        if (gameId.Equals(null))
        {
            rounds = await _gameRoundService.GetAllAsync();
        }
        else
        {
            rounds = await _gameRoundService.GetAllByGameIdAsync((Guid)gameId);
        }
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
            PreviousPlaces = await _gameRoundService.GetDistinctPlacesAsync(),
            PreviousOpponents = await _gameRoundService.GetDistinctPlayersAsync(),
            GameRound = new CreateGameRoundModel{GameId = gameId, Game = await _gameService.GetByIdAsync(gameId)},
            Users = await _userService.GetAllAsync()
        };
        return View(createViewModel);
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