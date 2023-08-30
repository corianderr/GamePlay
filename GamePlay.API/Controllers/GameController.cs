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
    private const string GameCoverDefaultPath = "/gameCovers/default-game-cover.jpg";
    private const string GameCoverDirectoryName = "gameCovers";

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
            createModel.GameModel = JsonConvert.DeserializeObject<CreateGameModel>(createModel.GameModelJson!);

            var validationResults = Validate(createModel.GameModel, out var isValid);
            if (isValid) {
                var photoPath =
                    ImageUploadingHelper.GeneratePhotoPath(GameCoverDirectoryName, createModel.GameImage, GameCoverDefaultPath);
                createModel.GameModel!.PhotoPath = photoPath;

                await _gameService.CreateAsync(createModel.GameModel);
                if (!photoPath.Equals(GameCoverDefaultPath)) {
                    await ImageUploadingHelper.UploadAsync(createModel.GameImage!, photoPath);
                }
            }
            else {
                return Ok(ApiResult<CreateGameModel>.Failure(validationResults.Select(r => r.ErrorMessage)));
            }
        }
        catch (ArgumentException ex) {
            ModelState.AddModelError("Name", ex.Message);
            return Ok(ApiResult<CreateGameModel>.Failure(new[] { ex.Message }));
        }

        return Ok(ApiResult<CreateGameModel>.Success(createModel.GameModel));
    }

    // PUT: Games/Edit/5
    [Authorize(Roles = "admin")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Edit(Guid id, [FromForm] UpdateGameViewModel updateModel) {
        try {
            updateModel.GameModel = JsonConvert.DeserializeObject<CreateGameModel>(updateModel.GameModelJson!);

            var validationResults = Validate(updateModel.GameModel, out var isValid);
            if (isValid) {
                var photoPath =
                    ImageUploadingHelper.GeneratePhotoPath(GameCoverDirectoryName, updateModel.GameImage, GameCoverDefaultPath);
                var previousPath = updateModel.GameModel!.PhotoPath;
                updateModel.GameModel!.PhotoPath = photoPath;

                await _gameService.UpdateAsync(id, updateModel.GameModel);
                await ImageUploadingHelper.ReuploadAsync(photoPath, GameCoverDefaultPath, updateModel.GameImage!,
                    previousPath!);
            }
            else {
                return Ok(ApiResult<CreateGameModel>.Failure(validationResults.Select(r => r.ErrorMessage)));
            }
        }
        catch (ArgumentException ex) {
            ModelState.AddModelError("Name", ex.Message);
            return Ok(ApiResult<CreateGameModel>.Failure(new[] { ex.Message }));
        }

        return Ok(ApiResult<Guid>.Success(id));
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

    private static IEnumerable<ValidationResult> Validate<TModel>(TModel model, out bool isValid) {
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        isValid = Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }
}