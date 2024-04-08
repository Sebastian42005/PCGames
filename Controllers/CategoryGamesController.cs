using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCGamesFinal.Data;
using PC_Spiele.Models;
using Microsoft.AspNetCore.Authorization;

namespace PCGamesFinal.Controllers
{
    public class CategoryGamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryGamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryGames
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CategoryGames.Include(c => c.Category).Include(c => c.Game);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CategoryGames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryGames = await _context.CategoryGames
                .Include(c => c.Category)
                .Include(c => c.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryGames == null)
            {
                return NotFound();
            }

            return View(categoryGames);
        }

        // GET: CategoryGames/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            ViewData["Category_id"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Name");
            return View();
        }

        // POST: CategoryGames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category_id,Game_id")] CategoryGames categoryGames)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryGames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category_id"] = new SelectList(_context.Category, "Id", "Name", categoryGames.Category_id);
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Name", categoryGames.Game_id);
            return View(categoryGames);
        }

        // GET: CategoryGames/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryGames = await _context.CategoryGames.FindAsync(id);
            if (categoryGames == null)
            {
                return NotFound();
            }
            ViewData["Category_id"] = new SelectList(_context.Category, "Id", "Name", categoryGames.Category_id);
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Name", categoryGames.Game_id);
            return View(categoryGames);
        }

        // POST: CategoryGames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category_id,Game_id")] CategoryGames categoryGames)
        {
            if (id != categoryGames.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryGames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryGamesExists(categoryGames.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category_id"] = new SelectList(_context.Category, "Id", "Name", categoryGames.Category_id);
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Name", categoryGames.Game_id);
            return View(categoryGames);
        }

        // GET: CategoryGames/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryGames = await _context.CategoryGames
                .Include(c => c.Category)
                .Include(c => c.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryGames == null)
            {
                return NotFound();
            }

            return View(categoryGames);
        }

        // POST: CategoryGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryGames = await _context.CategoryGames.FindAsync(id);
            if (categoryGames != null)
            {
                _context.CategoryGames.Remove(categoryGames);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryGamesExists(int id)
        {
            return _context.CategoryGames.Any(e => e.Id == id);
        }
    }
}
