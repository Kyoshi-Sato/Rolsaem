using Microsoft.EntityFrameworkCore;
using BarApp.Models;

namespace BarApp.Data
{
    public class BarContext : DbContext
    {
        public BarContext(DbContextOptions<BarContext> options) : base(options) { }

        public DbSet<Bebida> Bebidas { get; set; }
    }
}
