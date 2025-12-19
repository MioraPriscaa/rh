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

    public DbSet<Candidature> Candidature { get; set; }
    public DbSet<Candidat> Candidat { get; set; }

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

            modelBuilder.Entity<Candidature>()
               .HasOne(c => c.Candidat)
               .WithMany(c => c.Candidatures)
               .HasForeignKey(c => c.IdCandidat);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Annonce)
                .WithMany(a => a.Candidatures)
                .HasForeignKey(c => c.IdAnnonce);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Statut)
                .WithMany(s => s.Candidatures)
                .HasForeignKey(c => c.IdStatut);

            modelBuilder.Entity<Annonce>()
                .Property(a => a.NbDossierValide)
                .HasDefaultValue(0);

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
