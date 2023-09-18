using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Exceptions;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Player;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.API.Controllers;

[Authorize]
public class PlayerController : ApiController {
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService) {
        _playerService = playerService;
    }

    // GET: Player
    [HttpGet]
    public async Task<ActionResult> Index() {
        var games = await _playerService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<PlayerModel>>.Success(games));
    }

    // POST: Player/create
    [HttpPost("create")]
    public async Task<ActionResult> Create(CreatePlayerModel playerModel) {
        try {
            var baseModel = await _playerService.CreateAsync(playerModel);
            return Ok(ApiResult<BaseModel>.Success(baseModel));
        }
        catch (ArgumentException ex) {
            return Ok(ApiResult<string>.Failure(new[] { ex.Message }));
        }
    }

    // POST: Player/Delete/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id) {
        try {
            await _playerService.DeleteAsync(id);
        }
        catch (BadRequestException ex) {
            return Ok(ApiResult<string>.Failure(new[] { ex.Message }));
        }

        return Ok(ApiResult<BaseModel>.Success(new BaseModel { Id = id }));
    }
}