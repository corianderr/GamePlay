using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models.GameRound;
using GamePlay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    // GET: GameRounds/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var round = await _gameRoundService.GetByIdAsync(id);
        return View(round);
    }
    
    // GET: GameRounds/Create
    [Authorize(Roles = "admin")]
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
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateGameRoundViewModel createViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return View(createViewModel);

            var roundId = (await _gameRoundService.AddAsync(createViewModel.GameRound)).Id;
            return Json(new { success = true, redirectToUrl = Url.Action("Details", "GameRounds", new {id = roundId})});
        }
        catch
        {
            return View(createViewModel);
        }
    }
    
    // GET: GameRounds/Edit
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Edit(Guid gameRoundId)
    {
        var createViewModel = new UpdateGameRoundViewModel()
        {
            PreviousPlaces = await _gameRoundService.GetDistinctPlacesAsync(),
            PreviousOpponents = await _gameRoundService.GetDistinctPlayersAsync(),
            GameRound = await _gameRoundService.GetByIdAsync(gameRoundId),
            Users = await _userService.GetAllAsync()
        };
        return View(createViewModel);
    }

    // POST: GameRounds/Edit
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid id, UpdateGameRoundViewModel updateViewModel)
    {
        try
        {
            if (!ModelState.IsValid) return View(updateViewModel);

            await _gameRoundService.UpdateAsync(id, updateViewModel.GameRound);
            return Json(new { success = true, redirectToUrl = Url.Action("Details", "GameRounds", new {id})});
        }
        catch
        {
            return View(updateViewModel);
        }
    }
    
    // GET: GameRounds/Delete/5
    public async Task<ActionResult> Delete(Guid id)
    {
        var collection = await _gameRoundService.GetByIdAsync(id);
        return View(collection);
    }

    // POST: GameRounds/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await _gameRoundService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

}