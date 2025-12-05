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
}
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Remplace la chaîne de connexion par la tienne
            optionsBuilder.UseSqlServer("Server=.;Database=rh_db;Trusted_Connection=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
