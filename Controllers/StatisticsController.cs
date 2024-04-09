using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PC_Spiele.Models;
using PCGamesFinal.Data;
using PCGamesFinal.Models;
using System.Collections;
using System.Runtime.Intrinsics.Arm;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PCGamesFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
			var games = await _context.Game.ToListAsync();
			var dataPoints = await _context.GameOrders
            .Where(go => !string.IsNullOrEmpty(go.Order.Invoice))
            .GroupBy(go => new { go.Game.Id, go.Game.Name })
            .OrderByDescending(g => g.Sum(go => go.Amount))
            .Take(5)
            .Select(g => new DataPoint(g.Key.Id, g.Key.Name, g.Sum(go => go.Amount)))
            .ToListAsync();

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

			return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }

			var gameOrders = await _context.GameOrders
			.Where(go => go.Game_id == game.Id)
            .Include(go => go.Order)
			.ToListAsync();

            gameOrders.Sort((x, y) => x.Order.Date.CompareTo(y.Order.Date));

			var detailDataPoints = new List<DetailDataPoint>();

            gameOrders.ForEach(go =>
            {
                var currentDataPoint = detailDataPoints.FirstOrDefault(dp => dp.date.DayOfYear == go.Order.Date.DayOfYear);
                if (currentDataPoint != null)
                {
                    detailDataPoints.Remove(currentDataPoint);
                    currentDataPoint.Y = currentDataPoint.Y + go.Amount;
                    detailDataPoints.Add(currentDataPoint);
				}
				else
                {
					DateTime date = go.Order.Date; 

					long unixTimeMilliseconds = (long)(date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

					var newDetailPoint = new DetailDataPoint(unixTimeMilliseconds, go.Amount);
                    newDetailPoint.date = go.Order.Date;
                    detailDataPoints.Add(newDetailPoint);
                }
            });

			ViewBag.DetailDataPoints = JsonConvert.SerializeObject(detailDataPoints);

			return View(game);
        }


    }
}
