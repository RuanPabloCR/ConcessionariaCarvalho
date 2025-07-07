using Domain.Entities;

namespace Application.RepositoriesInterface
{
    public interface IRegisterSalesPersonRepository
    {
        Task<SalesPerson?> GetByEmailAsync(string email);
        Task AddAsync(SalesPerson salesPerson);
    }
}
