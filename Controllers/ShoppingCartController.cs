using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_Spiele.Models;
using PCGamesFinal.Data;

namespace PCGamesFinal.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return Forbid();
            }

            var order = await _context.Order.Include(go => go.user).FirstOrDefaultAsync(o => o.UserId == user.Id && o.Invoice == "");
            if (order == null)
            {
                return View(new List<GameOrders>());
            }

            var gameOrders = await _context.GameOrders.Include(go => go.Game).Include(go => go.Order).Where(go => go.Order_id == order.Id).ToListAsync();

            return View(gameOrders);
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOrder = await _context.GameOrders
                .FirstOrDefaultAsync(go => go.Id == id);
            if (gameOrder == null)
            {
                return NotFound();
            }

            _context.GameOrders.Remove(gameOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Orders
        public async Task<IActionResult> Buy()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return Forbid();
            }
            var order = await _context.Order.Include(go => go.user).FirstOrDefaultAsync(o => o.UserId == user.Id && o.Invoice == "");
            if (order == null) {
                return NotFound();
            }
            order.Invoice = Guid.NewGuid().ToString();
            _context.Update(order);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Items were successfully bought";
            return RedirectToAction(nameof(Index));
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
