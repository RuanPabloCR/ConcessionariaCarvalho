using Application.RepositoriesInterface;
using Infraestructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infraestructure.Repositories
{
    public class RegisterSalesPersonRepository : IRegisterSalesPersonRepository
    {
        private readonly AppDbContext _context;
        public RegisterSalesPersonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(SalesPerson salesPerson)
        {
            _context.SalesPeople.Add(salesPerson);
            await _context.SaveChangesAsync();
        }
        public async Task<SalesPerson?> GetByEmailAsync(string email)
        {
            return await _context.SalesPeople.FirstOrDefaultAsync(sp => sp.Email == email);
        }
    }
}
