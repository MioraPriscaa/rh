using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using rh.Domain.Entities;
namespace rh.Infrastructure.Data
{
    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Annonce> Annonces { get; set; }
    public DbSet<TypeContrat> TypeContrats { get; set; }

    public DbSet<ModeTravail> ModeTravails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation Annonce -> TypeContrat
            modelBuilder.Entity<Annonce>()
                .HasOne(a => a.TypeContrat)
                .WithMany(t => t.Annonces)
                .HasForeignKey(a => a.IdTypeContrat);

            // Relation Annonce -> ModeTravail
            modelBuilder.Entity<Annonce>()
                .HasOne(a => a.ModeTravail)
                .WithMany(m => m.Annonces)
                .HasForeignKey(a => a.IdModeTravail);
        }

    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Remplace la chaîne de connexion par la tienne
            optionsBuilder.UseSqlServer("Server=.;Database=rh_db;Trusted_Connection=True;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
