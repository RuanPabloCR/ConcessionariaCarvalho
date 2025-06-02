using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Entities.Car> Cars { get; set; }
        public DbSet<Domain.Entities.Guest> Guests { get; set; }
        public DbSet<Domain.Entities.SalesPerson> SalesPeople { get; set; }
        public DbSet<Domain.Entities.Sale> SalesProducts { get; set; }
        
    }
}
