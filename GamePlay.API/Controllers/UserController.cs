using GamePlay.API.ViewModels;
using GamePlay.BLL.Helpers;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Enums;
using GamePlay.Domain.Models;
using GamePlay.Domain.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.API.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IUserService _userService;
    private readonly IUserRelationService _relationService;
    private readonly ICollectionService _collectionService;

    public UserController(IUserService userService, IUserRelationService relationService, ICollectionService collectionService)
    {
        _userService = userService;
        _relationService = relationService;
        _collectionService = collectionService;
    }

    // GET: Users
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return Ok(ApiResult<IEnumerable<UserModel>>.Success(users));
    }
    
    // GET: Users/Details/5
    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(string id)
    {
        var userRelation = await _relationService.GetByUsersIdAsync(id, User.Identity.GetUserId());

        var userDetailsViewModel = new UserDetailsViewModel();
        if (userRelation == null)
        {
            var oppositeRelation = await _relationService.GetByUsersIdAsync(User.Identity.GetUserId(), id);
            userDetailsViewModel.RelationOption = oppositeRelation == null
                ? RelationOptions.DoesNotExist
                : oppositeRelation.IsFriend
                    ? RelationOptions.Friends
                    : RelationOptions.Pending;
        }
        else
        {
            userDetailsViewModel.RelationOption = userRelation.IsFriend
                ? RelationOptions.Friends
                : RelationOptions.Accept;
        }

        userDetailsViewModel.User = await _userService.GetFirstAsync(id);
        userDetailsViewModel.Collections = await _collectionService.GetAllAsync(c => c.UserId.Equals(id));
        
        userDetailsViewModel.IsCurrentUser = User.Identity.GetUserId().Equals(userDetailsViewModel.User.Id);
        return Ok(ApiResult<UserDetailsViewModel>.Success(userDetailsViewModel));
    }
    
    // PUT: Users/Edit
    [HttpPut]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(UserModel userModel, IFormFile? avatar)
    {
        userModel.PhotoPath = await ImageUploadingHelper.ReuploadAndGetNewPathAsync("avatars",
            "/avatars/default-user-avatar.jpg", avatar, userModel.PhotoPath);

        await _userService.UpdateAsync(userModel.Id, userModel);
        return Ok(ApiResult<UserModel>.Success(userModel));
    }
    
    // POST: Users/Follow
    [HttpPost("Follow")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Follow(string id)
    {
        await _relationService.SubscribeAsync(User.Identity.GetUserId(), id);
        return Ok(ApiResult<string>.Success(id));
    }

    // POST: Users/BecomeFriends
    [HttpPost("BecomeFriends")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> BecomeFriends(string id)
    {
        await _relationService.BecomeFriendsAsync(id, User.Identity.GetUserId());
        return Ok(ApiResult<string>.Success(id));
    }
    
    // GET: Users/ShowRelations/2&true
    [HttpGet("ShowRelations/{userId}&{isFriend:bool}")]
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

        return Ok(ApiResult<IEnumerable<ApplicationUser?>>.Success(users));
    }

    // GET: Users/ShowNotifications
    [HttpGet("ShowNotifications")]
    public async Task<ActionResult> ShowNotifications()
    {
        var subscribers = (await _relationService.GetAllAsync(User.Identity.GetUserId(), false)).Select(r => r.Subscriber);
        return Ok(ApiResult<IEnumerable<ApplicationUser?>>.Success(subscribers));
    }
}