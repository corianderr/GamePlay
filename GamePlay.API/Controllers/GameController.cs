using System.ComponentModel.DataAnnotations;
using GamePlay.API.ViewModels;
using GamePlay.BLL.Helpers;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamePlay.API.Controllers;

public class GameController : ApiController {
    private readonly IGameService _gameService;
    private readonly IGameRatingService _ratingService;
    private readonly ICollectionService _collectionService;

    public GameController(IGameService gameService, IGameRatingService ratingService, ICollectionService collectionService) {
        _gameService = gameService;
        _ratingService = ratingService;
        _collectionService = collectionService;
    }

    // GET: Games
    [HttpGet]
    public async Task<ActionResult> Index() {
        var games = await _gameService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<GameModel>>.Success(games));
    }

    // GET: Games/Details/5
    [HttpGet("details/{id:guid}")]
    public async Task<ActionResult> Details(Guid id) {
        var gameDetailsViewModel = new GameDetailsViewModel() {
            Rating = await _ratingService.GetByUserAndGameAsync(User.Identity.GetUserId(), id),
            Game = await _gameService.GetByIdAsync(id),
            AvailableCollections = await _collectionService.GetAllWhereMissing(User.Identity.GetUserId(), id)
        };
        return Ok(ApiResult<GameDetailsViewModel>.Success(gameDetailsViewModel));
    }

    [HttpGet("getById/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) {
        var game = await _gameService.GetByIdAsync(id);
        return Ok(ApiResult<GameModel>.Success(game));
    }

    // POST: Games/
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateGameViewModel createModel) {
        try {
            createModel.GameModel = JsonConvert.DeserializeObject<CreateGameModel>(createModel.GameModelJson);

            var validationResults = Validate(createModel.GameModel, out var isValid);
            if (isValid) {
                createModel.GameModel.PhotoPath =
                    await ImageUploadingHelper.UploadImageAsync("gameCovers", "/gameCovers/default-game-cover.jpg",
                        createModel.GameImage);
                await _gameService.CreateAsync(createModel.GameModel);
            }
            else {
                return Ok(ApiResult<CreateGameModel>.Failure(validationResults.Select(r => r.ErrorMessage)));
            }
        }
        catch (ArgumentException ex) {
            ModelState.AddModelError("Name", ex.Message);
        }

        return Ok(ApiResult<CreateGameModel>.Success(createModel.GameModel));
    }

    private static IEnumerable<ValidationResult> Validate<TModel>(TModel model, out bool isValid) {
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        isValid = Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }

    // PUT: Games/Edit/5
    [Authorize(Roles = "admin")]
    [HttpPut]
    public async Task<ActionResult> Edit(Guid id, GameModel gameModel, IFormFile? gameImage) {
        gameModel.PhotoPath =
            await ImageUploadingHelper.ReuploadAndGetNewPathAsync("gameCovers",
                "/gameCovers/default-game-cover.jpg", gameImage, gameModel.PhotoPath);

        await _gameService.UpdateAsync(id, gameModel);
        return Ok(ApiResult<GameModel>.Success(gameModel));
    }

    // POST: Games/Delete/5
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id) {
        await _gameService.DeleteAsync(id);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel() { Id = id }));
    }

    // POST: Games/RateGame
    [Authorize]
    [HttpPost("rateGame")]
    public async Task<ActionResult> RateGame(Guid id, int rating) {
        var gameRating = new CreateGameRatingModel {
            GameId = id,
            UserId = User.Identity.GetUserId(),
            Rating = rating
        };
        return Ok(ApiResult<BaseModel>.Success(await _ratingService.AddAsync(gameRating)));
    }

    // POST: Games/DeleteRating/5
    [Authorize]
    [HttpDelete("deleteRating/{id:guid}")]
    public async Task<ActionResult> DeleteRating(Guid id) {
        var gameId = (await _ratingService.GetByIdAsync(id)).GameId;
        await _ratingService.DeleteAsync(id);

        return Ok(ApiResult<BaseModel>.Success(new BaseModel() { Id = (Guid)gameId }));
    }
}