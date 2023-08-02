using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.Collection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.API.Controllers;

[Authorize]
public class CollectionController : ApiController
{
    private readonly ICollectionService _collectionService;

    public CollectionController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    // GET: Collections/Details/5
    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult> Details(Guid id)
    {
        var collection = await _collectionService.GetByIdAsync(id);
        return Ok(ApiResult<CollectionModel>.Success(collection));
    }
    
    // POST: Collections/AddGame
    [HttpPost("AddGame")]
    public async Task<ActionResult> AddGame(Guid id, Guid collectionId)
    {
        await _collectionService.AddGameAsync(id, collectionId);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel(){Id = collectionId}));
    }
    
    // POST: Collections/DeleteFromCollection
    [HttpPost("DeleteGame")]
    public async Task<ActionResult> DeleteFromCollection(Guid id, Guid collectionId)
    {
        await _collectionService.DeleteGameAsync(id, collectionId);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel(){Id = collectionId}));
    }

    // POST: Collections/Create
    [HttpPost]
    public async Task<ActionResult> Create(CreateCollectionModel collectionModel)
    {
        try
        {
            collectionModel.UserId = User.Identity.GetUserId();
            await _collectionService.CreateAsync(collectionModel);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Name", ex.Message);
        }
        return Ok(ApiResult<CreateCollectionModel>.Success(collectionModel));
    }
    
    // POST: Collections/Edit/5
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Edit(Guid id, CollectionModel collectionModel)
    {
        await _collectionService.UpdateAsync(id, collectionModel);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel{Id = id}));
    }
    
    // POST: Collections/Delete/5
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
        await _collectionService.DeleteAsync(id);
        return Ok(ApiResult<BaseModel>.Success(new BaseModel{Id = id}));
    }
}