using Domain.Entities;
namespace Application.RepositoriesInterface
{
    public interface IGetSalesPersonRepository
    {
        Task<SalesPerson?> GetSalesPersonByIdAsync(Guid id);
    }
}
