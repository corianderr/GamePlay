using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlay.BLL.Services.Interfaces;
using GamePlay.Domain.Models.User;
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
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var userRelation = await _userService.GetRelationByUsersIdAsync(id, User.Identity.GetUserId());

            var relation = new UserRelationViewModel();
            if (userRelation == null) relation.IsFriend = null;
            
            relation.User = await _userService.GetFirstAsync(id);
            ViewBag.IsCurrentUser = User.Identity.GetUserId().Equals(relation.User.Id);
            return View(relation);
        }
    }
}