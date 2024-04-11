using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PCGamesFinal.Data;
using PC_Spiele.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PCGamesFinal.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GamesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Game.Include(g => g.PG).Include(g => g.Publisher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.PG)
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);

            var categories = _context.CategoryGames
                .Where(go => go.Game_id == game.Id)
                .Include(go => go.Category)
            .Select(go => go.Category);

            ViewBag.Categories = categories;

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityUser user = await GetCurrentUserAsync();
            
            if (user == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.PG)
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(o => o.UserId == user.Id && (o.Invoice == null || o.Invoice == ""));

            if (order == null)
            {
                order = new Order
                {
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Invoice = ""
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
            }

            var gameOrder = await _context.GameOrders
                .FirstOrDefaultAsync(go => go.Game_id == game.Id && go.Order_id == order.Id);

            if (gameOrder == null)
            {
                gameOrder = new GameOrders
                {
                    Game_id = game.Id,
                    Order_id = order.Id,
                    Amount = 1
                };
                _context.Add(gameOrder);
                await _context.SaveChangesAsync();
            } else
            {
                gameOrder.Amount++;
                _context.Update(gameOrder);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Game " + game.Name + " was added to the shopping cart";
            return RedirectToAction(nameof(Index));
        }



        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Games/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            ViewData["PG_id"] = new SelectList(_context.Set<PG>(), "Id", "Name");
            ViewData["Publisher_id"] = new SelectList(_context.Publisher, "Id", "Name");

            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,Price,PG_id,Publisher_id", "selectedCategories")] GameDto gameDto)
        {
            Game game = new Game();
            game.Name = gameDto.Name;
            game.Id = gameDto.Id;
            game.Price = gameDto.Price;
            game.Amount = gameDto.Amount;
            game.Publisher_id = gameDto.Publisher_id;
            game.Publisher = gameDto.Publisher;
            game.PG_id = gameDto.PG_id;
            game.PG = gameDto.PG;

            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();

                if (gameDto.selectedCategories != null)
                {
                    foreach (int categoryId in gameDto.selectedCategories)
                    {
                        _context.Add(new CategoryGames { Category_id = categoryId, Game_id = game.Id });
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["PG_id"] = new SelectList(_context.Set<PG>(), "Id", "Name", game.PG_id);
            ViewData["Publisher_id"] = new SelectList(_context.Publisher, "Id", "Name", game.Publisher_id);

            // Pass the list of categories to the view
            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");

            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["PG_id"] = new SelectList(_context.Set<PG>(), "Id", "Name", game.PG_id);
            ViewData["Publisher_id"] = new SelectList(_context.Publisher, "Id", "Name", game.Publisher_id);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Amount,Price,PG_id,Publisher_id")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            ViewData["PG_id"] = new SelectList(_context.Set<PG>(), "Id", "Name", game.PG_id);
            ViewData["Publisher_id"] = new SelectList(_context.Publisher, "Id", "Name", game.Publisher_id);
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.PG)
                .Include(g => g.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game != null)
            {
                _context.Game.Remove(game);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
