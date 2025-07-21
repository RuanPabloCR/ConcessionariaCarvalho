using Application.RepositoriesInterface;
using Domain.Entities;
using Infraestructure.Data;

namespace Infraestructure.Repositories.SalesPersonR
{
    public class GetSalesPersonRepository : IGetSalesPersonRepository
    {
        private readonly AppDbContext _context;
        public GetSalesPersonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SalesPerson?> GetSalesPersonByIdAsync(Guid id) 
        {
            return await _context.SalesPeople.FindAsync(id);
        }
    }
}
