using Application.RepositoriesInterface;
using Domain.Entities;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class CreateCarRepository : ICreateCarRepository
    {
        private readonly AppDbContext _context;
        public CreateCarRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task RegisterCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

    }
}
