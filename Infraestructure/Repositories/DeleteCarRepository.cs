using Application.RepositoriesInterface;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class DeleteCarRepository : IDeleteCarRepository
    {
        private readonly AppDbContext _context;
        public DeleteCarRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteCarAsync(Guid id)
        {
            
                var car = await _context.Cars.FindAsync(id);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    await _context.SaveChangesAsync();
                }
        }
    }
}
