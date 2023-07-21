using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Enums;
using GamePlay.Domain.Models.User;
using GamePlay.Web.Helpers;
using GamePlay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRelationService _relationService;

    public UsersController(IUserService userService, IUserRelationService relationService)
    {
        _userService = userService;
        _relationService = relationService;
    }

    // GET: Users
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync(u => !u.Id.Equals(User.Identity.GetUserId()));
        return View(users);
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(string id)
    {
        var userRelation = await _relationService.GetByUsersIdAsync(id, User.Identity.GetUserId());

        var userDetailsViewModel = new UserDetailsViewModel();
        if (userRelation == null)
        {
            var oppositeRelation = await _relationService.GetByUsersIdAsync(User.Identity.GetUserId(), id);
            if (oppositeRelation == null)
            {
                userDetailsViewModel.RelationOption = RelationOptions.DoesNotExist;
            }
            else
            {
                userDetailsViewModel.RelationOption = oppositeRelation.IsFriend ? RelationOptions.Friends : RelationOptions.Pending;
            }
        }
        else
        {
            userDetailsViewModel.RelationOption = userRelation.IsFriend ? RelationOptions.Friends : RelationOptions.Accept;
        }

        userDetailsViewModel.User = await _userService.GetFirstAsync(id);
        userDetailsViewModel.Games = (await _userService.GetUsersGames(id)).ToList();
        
        ViewBag.IsCurrentUser = User.Identity.GetUserId().Equals(userDetailsViewModel.User.Id);
        return View(userDetailsViewModel);
    }

    // GET: Users/Edit
    public async Task<IActionResult> Edit()
    {
        var user = await _userService.GetFirstAsync(User.Identity.GetUserId());
        return View(user);
    }

    // POST: Users/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UserModel userModel, IFormFile? avatar)
    {
        try
        {
            if (!ModelState.IsValid) return View(userModel);

            userModel.PhotoPath = await ImageUploadingHelper.ReuploadAndGetNewPathAsync("avatars",
                "/avatars/default-user-avatar.jpg", avatar, userModel.PhotoPath);

            await _userService.UpdateAsync(userModel.Id, userModel);
            return RedirectToAction(nameof(Details), new { id = userModel.Id });
        }
        catch
        {
            return View();
        }
    }

    // POST: Users/Follow/5
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<ActionResult> Follow(string id)
    {
        await _relationService.SubscribeAsync(User.Identity.GetUserId(), id);
        return RedirectToAction(nameof(Details), new { id });
    }

    // POST: Users/BecomeFriends/5
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<ActionResult> BecomeFriends(string id)
    {
        await _relationService.BecomeFriendsAsync(id, User.Identity.GetUserId());
        return RedirectToAction(nameof(Details), new { id });
    }

    // GET: Users/ShowRelations/userId=2&isFriend=true
    public async Task<ActionResult> ShowRelations(string userId, bool isFriend)
    {
        IEnumerable<ApplicationUser?> users;
        if (isFriend)
        {
            users = (await _relationService.GetAllAsync(userId, isFriend)).SelectMany(r =>
                new[] { r.Subscriber, r.User });
            users = users.Where(u => !u.Id.Equals(userId));
        }
        else
        {
            users = (await _relationService.GetAllAsync(userId, isFriend)).Select(r => r.Subscriber);
        }

        return View(users);
    }

    // GET: Users/ShowNotifications
    public async Task<ActionResult> ShowNotifications()
    {
        var subscribers = (await _relationService.GetAllAsync(User.Identity.GetUserId(), false)).Select(r => r.Subscriber);
        return View(subscribers);
    }

    // Get: Users/AddGame/5
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddGame(Guid id)
    {
        string currentUserId = User.Identity.GetUserId();
        await _userService.AddGameToUserAsync(id, currentUserId);
        return RedirectToAction(nameof(Details), new { id = currentUserId });
    }
}