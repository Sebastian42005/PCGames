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
    public class PGsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PGsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PGs
        public async Task<IActionResult> Index()
        {
            return View(await _context.PG.ToListAsync());
        }

        // GET: PGs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pG = await _context.PG
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pG == null)
            {
                return NotFound();
            }

            return View(pG);
        }

        // GET: PGs/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PG pG)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pG);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pG);
        }

        // GET: PGs/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pG = await _context.PG.FindAsync(id);
            if (pG == null)
            {
                return NotFound();
            }
            return View(pG);
        }

        // POST: PGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PG pG)
        {
            if (id != pG.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pG);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PGExists(pG.Id))
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
            return View(pG);
        }

        // GET: PGs/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pG = await _context.PG
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pG == null)
            {
                return NotFound();
            }

            return View(pG);
        }

        // POST: PGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pG = await _context.PG.FindAsync(id);
            if (pG != null)
            {
                _context.PG.Remove(pG);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PGExists(int id)
        {
            return _context.PG.Any(e => e.Id == id);
        }
    }
}
