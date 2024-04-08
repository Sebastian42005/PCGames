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
    public class GameOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GameOrders.Include(g => g.Game).Include(g => g.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GameOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOrders = await _context.GameOrders
                .Include(g => g.Game)
                .Include(g => g.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameOrders == null)
            {
                return NotFound();
            }

            return View(gameOrders);
        }

        // GET: GameOrders/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Name");
            ViewData["Order_id"] = new SelectList(_context.Order, "Id", "Id");
            return View();
        }

        // POST: GameOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Game_id,Order_id")] GameOrders gameOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Name", gameOrders.Game_id);
            ViewData["Order_id"] = new SelectList(_context.Order, "Id", "Id", gameOrders.Order_id);
            return View(gameOrders);
        }

        // GET: GameOrders/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOrders = await _context.GameOrders.FindAsync(id);
            if (gameOrders == null)
            {
                return NotFound();
            }
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Id", gameOrders.Game_id);
            ViewData["Order_id"] = new SelectList(_context.Order, "Id", "Id", gameOrders.Order_id);
            return View(gameOrders);
        }

        // POST: GameOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Game_id,Order_id")] GameOrders gameOrders)
        {
            if (id != gameOrders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameOrdersExists(gameOrders.Id))
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
            ViewData["Game_id"] = new SelectList(_context.Game, "Id", "Id", gameOrders.Game_id);
            ViewData["Order_id"] = new SelectList(_context.Order, "Id", "Id", gameOrders.Order_id);
            return View(gameOrders);
        }

        // GET: GameOrders/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOrders = await _context.GameOrders
                .Include(g => g.Game)
                .Include(g => g.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameOrders == null)
            {
                return NotFound();
            }

            return View(gameOrders);
        }

        // POST: GameOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameOrders = await _context.GameOrders.FindAsync(id);
            if (gameOrders != null)
            {
                _context.GameOrders.Remove(gameOrders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameOrdersExists(int id)
        {
            return _context.GameOrders.Any(e => e.Id == id);
        }
    }
}
