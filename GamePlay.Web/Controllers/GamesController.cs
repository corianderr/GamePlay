using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models.Game;
using GamePlay.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;

public class GamesController : Controller
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
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
        var rating = await _gameService.GetRatingAsync(User.Identity.GetUserId(), id);
        ViewBag.Rating = rating == null ? null : (int?)rating.Rating;
        var game = await _gameService.GetByIdAsync(id);
        return View(game);
    }

    // GET: Games/Create
    [Authorize]
    public ActionResult Create()
    {
        return View();
    }

    // POST: Games/Create
    [Authorize]
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

            var response = await _gameService.CreateAsync(gameModel);
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
    [Authorize]
    public async Task<ActionResult> Edit(Guid id)
    {
        var game = await _gameService.GetByIdAsync(id);
        return View(game);
    }

    // POST: Games/Edit/5
    [Authorize]
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

            var response = await _gameService.UpdateAsync(id, gameModel);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: Games/Delete/5
    [Authorize]
    public async Task<ActionResult> Delete(Guid id)
    {
        var game = await _gameService.GetByIdAsync(id);
        return View(game);
    }

    // POST: Games/Delete/5
    [Authorize]
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
            await _gameService.AddRatingAsync(gameRating);
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Details), new { Id = id });
    }
}