using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Models.Collection;
using GamePlay.Domain.Models.Game;
using GamePlay.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;
[Authorize]
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
    
    // GET: Collections/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Collections/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateCollectionModel collectionModel)
    {
        try
        {
            if (!ModelState.IsValid) return View(collectionModel);

            collectionModel.UserId = User.Identity.GetUserId();
            await _collectionService.CreateAsync(collectionModel);
            return RedirectToAction(nameof(Details), "Users", new {id = collectionModel.UserId});
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Name", ex.Message);
            return View(collectionModel);
        }
        catch
        {
            return View(collectionModel);
        }
    }


    // GET: Collections/Edit/5
    public async Task<ActionResult> Edit(Guid id)
    {
        var collection = await _collectionService.GetByIdAsync(id);
        return View(collection);
    }

    // POST: Collections/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(Guid id, CollectionModel collectionModel)
    {
        try
        {
            if (!ModelState.IsValid) return View(collectionModel);
            
            await _collectionService.UpdateAsync(id, collectionModel);
            return RedirectToAction(nameof(Details), "Users", new {id = collectionModel.UserId});
        }
        catch
        {
            return View();
        }
    }

    // GET: Collections/Delete/5
    public async Task<ActionResult> Delete(Guid id)
    {
        var collection = await _collectionService.GetByIdAsync(id);
        return View(collection);
    }

    // POST: Collections/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
        try
        {
            await _collectionService.DeleteAsync(id);
            return RedirectToAction(nameof(Details), "Users", new {id = User.Identity.GetUserId()});
        }
        catch
        {
            return View();
        }
    }
}