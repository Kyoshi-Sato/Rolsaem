using Microsoft.EntityFrameworkCore;
using BarApp.Models;
using N1_Rosalem.Models;

namespace BarApp.Data
{
    public class BarContext : DbContext
    {
        public BarContext(DbContextOptions<BarContext> options) : base(options) { }

        public DbSet<Bebida> Bebidas { get; set; }
        public DbSet<UsuarioViewModel> Usuarios { get; set; } // Adicionando a entidade UsuarioViewModel
    }
}
