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
        
    }
}
