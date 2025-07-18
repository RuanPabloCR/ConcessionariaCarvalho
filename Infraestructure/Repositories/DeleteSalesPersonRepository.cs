using Application.RepositoriesInterface;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class DeleteSalesPersonRepository : IDeleteSalesPersonRepository
    {
        private readonly AppDbContext _context;
        public DeleteSalesPersonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(Guid salesPersonId)
        {
            var salesPerson = await _context.SalesPeople.FindAsync(salesPersonId);
            if (salesPerson != null)
            {
                _context.SalesPeople.Remove(salesPerson);
                await _context.SaveChangesAsync();
           
            }
        }
    }
}
