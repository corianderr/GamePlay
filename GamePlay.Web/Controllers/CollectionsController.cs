using GamePlay.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;

public class CollectionsController : Controller
{
    private readonly ICollectionService _collectionService;

    public CollectionsController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    // GET: Collections/Details/5
    public async Task<ActionResult> Details(Guid id)
    {
        var collection = await _collectionService.GetByIdAsync(id);
        return View(collection);
    }
    
    // POST: Collections/AddGame
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddGame(Guid id, Guid collectionId)
    {
        await _collectionService.AddGameAsync(id, collectionId);
        return RedirectToAction(nameof(Details), new { id = collectionId });
    }
    
    // POST: Collections/DeleteFromCollection
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteFromCollection(Guid id, Guid collectionId)
    {
        await _collectionService.DeleteGameAsync(id, collectionId);
        return RedirectToAction(nameof(Details), new { id = collectionId });
    }

    
}