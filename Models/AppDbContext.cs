using Microsoft.EntityFrameworkCore;
using SWGAU.Models.Modelos;
using SWGAU.Models.Enums;

namespace SWGAU.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Abono> Abonos { get; set; }
        public DbSet<Irrigacion> Irrigaciones { get; set; }
        public DbSet<Planta> Plantas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
        }
    }
}
