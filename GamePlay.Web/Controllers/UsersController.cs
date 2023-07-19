using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlay.BLL.Services.Interfaces;
using GamePlay.Domain.Models.User;
using GamePlay.Web.Enums;
using GamePlay.Web.Helpers;
using GamePlay.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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
            var userRelation = await _userService.GetRelationByUsersIdAsync(id, User.Identity.GetUserId());

            var relation = new UserRelationViewModel();
            if (userRelation == null)
            {
                var oppositeRelation = await _userService.GetRelationByUsersIdAsync(User.Identity.GetUserId(), id);
                if (oppositeRelation == null) relation.RelationOption = RelationOptions.DoesNotExist;
                else
                {
                    relation.RelationOption = oppositeRelation.IsFriend ? RelationOptions.Friends : RelationOptions.Pending;
                }
            }
            else relation.RelationOption = userRelation.IsFriend ? RelationOptions.Friends : RelationOptions.Accept;
            
            relation.User = await _userService.GetFirstAsync(id);
            ViewBag.IsCurrentUser = User.Identity.GetUserId().Equals(relation.User.Id);
            return View(relation);
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
        public async Task<ActionResult> Edit(UserResponseModel userModel, IFormFile? avatar)
        {
            try
            {
                if (!ModelState.IsValid) return View(userModel);

                userModel.PhotoPath = await ImageUploadingHelper.ReuploadAndGetNewPathAsync("avatars",
                    "/avatars/default-user-avatar.jpg", avatar, userModel.PhotoPath);
                
                var response = await _userService.UpdateAsync(userModel.Id, userModel);
                return RedirectToAction(nameof(Details), new {id = userModel.Id});
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
            await _userService.SubscribeAsync(User.Identity.GetUserId(), id);
            return RedirectToAction(nameof(Details), new {id});
        }
        
        // POST: Users/BecomeFriends/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> BecomeFriends(string id)
        {
            await _userService.BecomeFriendsAsync(id, User.Identity.GetUserId());
            return RedirectToAction(nameof(Details), new {id});
        }
    }
}