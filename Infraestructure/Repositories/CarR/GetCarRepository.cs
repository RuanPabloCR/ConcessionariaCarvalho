using Application.RepositoriesInterface;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
public class GetCarRepository : IGetCarRepository
{
    private readonly AppDbContext _context;

    public GetCarRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<IEnumerable<Car>> SearchAsync(string searchTerm)
    {
        return await _context.Cars
            .Where(c =>
                c.Brand.Contains(searchTerm) ||
                c.Model.Contains(searchTerm) ||
                (c.Brand + " " + c.Model + " " + c.Year).Contains(searchTerm)
            )
            .ToListAsync();
    }

    public async Task<IEnumerable<Car>> GetCarsByDateAsync(DateOnly date)
    {
        return await _context.Cars
            .Where(c => c.CreatedAt == date)
            .ToListAsync();
    }
}