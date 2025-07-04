using Microsoft.EntityFrameworkCore;
using ExamenLenguajesVisuales1.Models;

namespace ExamenLenguajesVisuales1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExamenLV1DB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
