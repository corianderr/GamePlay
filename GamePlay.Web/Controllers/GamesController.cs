using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlay.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamePlay.Web.Controllers
{
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
            return View(games.ToList());
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int id)
        {
        }

        // GET: Games/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: Games/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}