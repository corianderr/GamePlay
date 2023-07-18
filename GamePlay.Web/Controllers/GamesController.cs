using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePlay.BLL.Services.Interfaces;
using GamePlay.Domain.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        // GET: Games
        public async Task<ActionResult> Index()
        {
            
            var games = await _gameService.GetAllAsync();
            return View(games);
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var game = await _gameService.GetByIdAsync(id);
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateGameModel gameModel, IFormFile? gameImage)
        {
            try
            {
                if (!ModelState.IsValid) return View(gameModel);
                
                if (gameImage != null)
                {
                    var name = GenerateCode() + Path.GetExtension(gameImage.FileName);
                    gameModel.PhotoPath = "/gameCovers/" + name;
                    await using var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + gameModel.PhotoPath), FileMode.Create);
                    await gameImage.CopyToAsync(fileStream);
                }
                else gameModel.PhotoPath = "/gameCovers/default-game-cover.jpg";
                var response = await _gameService.CreateAsync(gameModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(gameModel);
            }
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var game = await _gameService.GetByIdAsync(id);
            return View(game);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, GameResponseModel gameModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(gameModel);
                
                var response = await _gameService.UpdateAsync(id, gameModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var game = await _gameService.GetByIdAsync(id);
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _gameService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        private static string GenerateCode()
        {
            var builder = new StringBuilder(12);
            var random = new Random();
            for (var i = 0; i < 12; i++)
            {
                builder.Append(random.Next(10));
            }
            return builder.ToString();
        }
    }
}