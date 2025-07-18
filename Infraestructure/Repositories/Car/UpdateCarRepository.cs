using Application.RepositoriesInterface;
using Infraestructure.Data;
using Domain.Entities;
namespace Infraestructure.Repositories.Car
{
    public class UpdateCarRepository : IUpdateCarRepository
    {
        private readonly AppDbContext _context;
        public UpdateCarRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateCarAsync(Guid id, Domain.Entities.Car car)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null)
            {
                return false;
            }
            existingCar.Model = car.Model;
            existingCar.Brand = car.Brand;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.Status = car.Status;
            _context.Cars.Update(existingCar);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Domain.Entities.Car?> GetCarByIdAsync(Guid id)
        {
            try
            {
                return await _context.Cars.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
