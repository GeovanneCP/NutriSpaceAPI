using Microsoft.EntityFrameworkCore;
using NutriSpaceAPI.Models;

namespace NutriSpaceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabelas que o .NET vai gerenciar:
        public DbSet<Estufa> Estufas { get; set; }
        public DbSet<LeituraSensor> LeiturasSensores { get; set; }
        public DbSet<Astronauta> Astronautas { get; set; }
        public DbSet<Planta> Plantas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<LeituraSensor>().Property(l => l.TemperaturaLida).HasPrecision(5, 2);
            modelBuilder.Entity<LeituraSensor>().Property(l => l.UmidadeLida).HasPrecision(5, 2);

            modelBuilder.Entity<Planta>().Property(p => p.TempMinIdeal).HasPrecision(5, 2);
            modelBuilder.Entity<Planta>().Property(p => p.TempMaxIdeal).HasPrecision(5, 2);
            modelBuilder.Entity<Planta>().Property(p => p.UmiMinIdeal).HasPrecision(5, 2);

            
            modelBuilder.Entity<LeituraSensor>()
                .Property(l => l.DtHrLeitura)
                .HasDefaultValueSql("SYSDATE");
        }
    }
}