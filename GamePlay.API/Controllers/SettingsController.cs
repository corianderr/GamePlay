using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.API.Controllers;

[Authorize(Roles = "admin")]
public class SettingsController : ApiController {
    private readonly IRecalculationService _recalculationService;

    public SettingsController(IRecalculationService recalculationService) {
        _recalculationService = recalculationService;
    }

    // GET: Game/Details/5
    [HttpPut("userRelations")]
    public async Task<ActionResult> RecalculateUserRelations() {
        try {
            await _recalculationService.RecalculateUserRelations();
            return Ok(ApiResult<string>.Success("Success"));
        }
        catch (Exception e) {
            return Ok(ApiResult<IEnumerable<string>>.Failure(new[] { e.Message }));
        }
    }
}