using GamePlay.API.ViewModels;
using GamePlay.Domain.Contracts.Services;
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
    [HttpGet]
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
    
    
}