using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models.Game;
using GamePlay.Web.Helpers;
using GamePlay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;

public class GamesController : Controller
{
    private readonly IGameService _gameService;
    private readonly IGameRatingService _ratingService;

    public GamesController(IGameService gameService, IGameRatingService ratingService)
    {
        _gameService = gameService;
        _ratingService = ratingService;
    }

    // GET: Games
    public async Task<ActionResult> Index()
    {
        var games = await _gameService.GetAllAsync();
        return View(games);
    }

    // GET: Games/Details/5
    public async Task<ActionResult> Details(Guid id)
    {
        var gameDetailsViewModel = new GameDetailsViewModel()
        {
            Rating = await _ratingService.GetByUserAndGameAsync(User.Identity.GetUserId(), id),
            Game = await _gameService.GetByIdAsync(id),
            // TODO: Fix to collections implementation
            // IsInCollection = await _gameService.CheckIfTheUserHas(User.Identity.GetUserId(), id)
        };
        return View(gameDetailsViewModel);
    }

    // GET: Games/Create
    [Authorize(Roles = "admin")]
    public ActionResult Create()
    {
        return View();
    }

    // POST: Games/Create
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateGameModel gameModel, IFormFile? gameImage)
    {
        try
        {
            if (!ModelState.IsValid) return View(gameModel);

            gameModel.PhotoPath =
                await ImageUploadingHelper.UploadImageAsync("gameCovers", "/gameCovers/default-game-cover.jpg",
                    gameImage);

            await _gameService.CreateAsync(gameModel);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Name", ex.Message);
            return View(gameModel);
        }
        catch
        {
            return View(gameModel);
        }
    }

    // GET: Games/Edit/5
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Edit(Guid id)
    {
        var game = await _gameService.GetByIdAsync(id);
        return View(game);
    }

    // POST: Games/Edit/5
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid id, GameModel gameModel, IFormFile? gameImage)
    {
        try
        {
            if (!ModelState.IsValid) return View(gameModel);

            gameModel.PhotoPath =
                await ImageUploadingHelper.ReuploadAndGetNewPathAsync("gameCovers",
                    "/gameCovers/default-game-cover.jpg", gameImage, gameModel.PhotoPath);

            await _gameService.UpdateAsync(id, gameModel);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Games/Delete/5
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var game = await _gameService.GetByIdAsync(id);
        return View(game);
    }

    // POST: Games/Delete/5
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await _gameService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // POST: Games/RateGame/5
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> RateGame(Guid id, int rating)
    {
        try
        {
            var gameRating = new CreateGameRatingModel
            {
                GameId = id,
                UserId = User.Identity.GetUserId(),
                Rating = rating
            };
            await _ratingService.AddAsync(gameRating);
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Details), new { Id = id });
    }

    // POST: Games/DeleteRating/5
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> DeleteRating(Guid id)
    {
        var gameId = (await _ratingService.GetByIdAsync(id)).GameId;
        try
        {
            await _ratingService.DeleteAsync(id);
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Details), new { Id = gameId });
    }
}