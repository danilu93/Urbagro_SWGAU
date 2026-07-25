using Microsoft.EntityFrameworkCore;
using SWGAU.Models.Modelos;
using SWGAU.Models.Enums;

namespace SWGAU.Models
{

    // Clase que representa el contexto de la base de datos de la aplicación
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Abono> Abonos { get; set; }
        public DbSet<Irrigacion> Irrigaciones { get; set; }
        public DbSet<Planta> Plantas { get; set; }

        // Configuración de las relaciones y restricciones de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       // Llamada al método base para mantener la configuración predeterminada
            base.OnModelCreating(modelBuilder);
            // Configuración de las entidades y sus relaciones
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Planta>().ToTable("Planta");
            modelBuilder.Entity<Abono>().ToTable("Abono");
            modelBuilder.Entity<Irrigacion>().ToTable("Irrigacion");
            
            
            modelBuilder.Entity<Usuario>() // Configuración de la entidad Usuario
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            // Configuración de la conversión de enumeraciones a cadenas para almacenar en la base de datos
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasConversion<String>();

            modelBuilder.Entity<Planta>()
                .Property(p => p.TipoPlanta)
                .HasConversion<String>();

            modelBuilder.Entity<Abono>()
                .Property(a => a.TipoAbono)
                .HasConversion<String>();

            modelBuilder.Entity<Abono>()
                .Property(a => a.FrecuenciaAbono)
                .HasConversion<String>();

            modelBuilder.Entity<Irrigacion>()
                .Property(i => i.MetodoRiego)
                .HasConversion<String>();

            modelBuilder.Entity<Irrigacion>()
                .Property(i => i.FrecuenciaRiego)
                .HasConversion<String>();


            // Configuración de las relaciones entre las entidades
            modelBuilder.Entity<Irrigacion>()
                .HasOne(i => i.Planta)
                .WithMany(p => p.Irrigaciones)
                .HasForeignKey(i => i.PlantaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Abono>()
                .HasOne(a => a.Planta)
                .WithMany(p => p.Abonos)
                .HasForeignKey(a => a.PlantaId)
                .OnDelete(DeleteBehavior.Cascade);

           

        }

    }
}
