using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PC_Spiele.Models;

namespace PCGamesFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PC_Spiele.Models.Game> Game { get; set; } = default!;
        public DbSet<PC_Spiele.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<PC_Spiele.Models.Category> Category { get; set; } = default!;
        public DbSet<PC_Spiele.Models.GameOrders> GameOrders { get; set; } = default!;
        public DbSet<PC_Spiele.Models.CategoryGames> CategoryGames { get; set; } = default!;
        public DbSet<PC_Spiele.Models.Order> Order { get; set; } = default!;
        public DbSet<PC_Spiele.Models.PG> PG { get; set; } = default!;
    }
}
