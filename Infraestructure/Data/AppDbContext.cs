using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<Sale> SalesProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Guest>()
                .Property(c => c.Balance)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Sale>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
