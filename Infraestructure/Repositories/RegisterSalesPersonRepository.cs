using Application.RepositoriesInterface;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RegisterSalesPersonRepository : IRegisterSalesPersonRepository
    {
        private readonly AppDbContext _context;
        public RegisterSalesPersonRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
