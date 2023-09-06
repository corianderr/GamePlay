using GamePlay.API.ViewModels;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Game;
using GamePlay.Domain.Models.GameRound;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.API.Controllers;

public class GameRoundController : ApiController {
    private readonly IGameRoundService _gameRoundService;
    private readonly IGameService _gameService;
    private readonly IUserService _userService;

    public GameRoundController(IGameRoundService gameRoundService, IGameService gameService, IUserService userService) {
        _gameRoundService = gameRoundService;
        _gameService = gameService;
        _userService = userService;
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> Index(Guid? gameId = null, string? userId = null) {
        IEnumerable<GameRoundModel> rounds;
        if (userId == null && gameId.Equals(null)) {
            rounds = await _gameRoundService.GetAllAsync();
        }
        else if (userId != null) {
            rounds = await _gameRoundService.GetAllAsync(r => r.Players.Any(p => p.UserId.Equals(userId)));
        }
        else if (!gameId.Equals(null)) {
            rounds = await _gameRoundService.GetAllByGameIdAsync((Guid)gameId);
        }
        else {
            rounds = await _gameRoundService.GetAllAsync(r => r.Players.Any(p => p.UserId.Equals(userId)) && r.GameId.Equals(gameId));
        }

        return Ok(ApiResult<IEnumerable<GameRoundModel>>.Success(rounds));
    }

    // GET: GameRounds/Details/5
    [Authorize]
    [HttpGet("getById/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) {
        var round = await _gameRoundService.GetByIdAsync(id);
        return Ok(ApiResult<GameRoundModel>.Success(round));
    }

    // GET: GameRounds/Create
    [Authorize(Roles = "admin")]
    [HttpGet("create/{gameId:guid}")]
    public async Task<ActionResult> Create(Guid gameId) {
        var createViewModel = new CreateGameRoundViewModel() {
            PreviousPlaces = await _gameRoundService.GetDistinctPlacesAsync(),
            PreviousOpponents = await _gameRoundService.GetDistinctPlayersAsync(),
            GameRound = new CreateGameRoundModel { GameId = gameId, Game = await _gameService.GetByIdAsync(gameId) },
            Users = await _userService.GetAllAsync()
        };
        return Ok(ApiResult<CreateGameRoundViewModel>.Success(createViewModel));
    }

    // POST: GameRounds
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult> Create(CreateGameRoundModel createViewModel) {
        var roundId = (await _gameRoundService.AddAsync(createViewModel)).Id;
        return Ok(ApiResult<BaseModel>.Success(new BaseModel() { Id = roundId }));
    }

    // GET: GameRounds/Edit
    [Authorize(Roles = "admin")]
    [HttpGet("edit/{gameRoundId:guid}")]
    public async Task<ActionResult> Edit(Guid gameRoundId) {
        var updateViewModel = new UpdateGameRoundViewModel() {
            PreviousPlaces = await _gameRoundService.GetDistinctPlacesAsync(),
            PreviousOpponents = await _gameRoundService.GetDistinctPlayersAsync(),
            GameRound = await _gameRoundService.GetByIdAsync(gameRoundId),
            Users = await _userService.GetAllAsync()
        };
        return Ok(ApiResult<UpdateGameRoundViewModel>.Success(updateViewModel));
    }

    // POST: GameRounds/Edit
    [Authorize(Roles = "admin")]
    [HttpPut("edit/{id:guid}")]
    public async Task<ActionResult> Edit(Guid id, GameRoundModel updateViewModel) {
        await _gameRoundService.UpdateAsync(id, updateViewModel);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel() { Id = id }));
    }

    // POST: GameRounds/Delete/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteConfirmed(Guid id) {
        await _gameRoundService.DeleteAsync(id);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel() { Id = id }));
    }
}