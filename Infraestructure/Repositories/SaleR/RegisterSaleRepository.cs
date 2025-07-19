using Application.RepositoriesInterface;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infraestructure.Repositories.SaleR
{
    public class SalesRepository : ISalesRepository
    {
        private readonly AppDbContext _context;
        public SalesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Sale>> GetAllSalesAsync(Guid salesPersonId)
        {
            return await _context.SalesProducts
                .Where(s => s.SalesPersonId == salesPersonId)
                .Include(s => s.Car)
                .Include(s => s.Guest)
                .ToListAsync();
        }
        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(Guid salesPersonId, DateTime start, DateTime end)
        {
            return await _context.SalesProducts
                .Where(s => s.SalesPersonId == salesPersonId && s.Date >= start && s.Date <= end)
                .Include(s => s.Car)
                .Include(s => s.Guest)
                .ToListAsync();
        }
        public async Task<bool> FindSaleAsync(Guid carId, Guid guestId, Guid salesPersonId)
        {
            return await _context.SalesProducts
                .AnyAsync(s => s.CarId == carId && s.GuestId == guestId && s.SalesPersonId == salesPersonId);
        }

        public async Task<bool> RegisterSaleAsync(Guid carId, Guid guestId, Guid salesPersonId, decimal price)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                var car = await _context.Cars.FindAsync(carId);
                if (car == null)
                    return false;


                var guest = await _context.Guests.FindAsync(guestId);
                if (guest == null)
                    return false;


                var salesPerson = await _context.SalesPeople.FindAsync(salesPersonId);
                if (salesPerson == null)
                    return false;


                if (car.Status != Domain.Enums.CarStatus.Available)
                    return false;

                var sale = new Sale
                {
                    Id = Guid.NewGuid(),
                    CarId = carId,
                    GuestId = guestId,
                    SalesPersonId = salesPersonId,
                    Price = price,
                    Date = DateTime.UtcNow
                };
                _context.SalesProducts.Add(sale);

                car.Status = Domain.Enums.CarStatus.Sold;
                _context.Cars.Update(car);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
