using Domain.Entities;
namespace Application.RepositoriesInterface
{
    public interface ISalesRepository
    {
        Task<bool> RegisterSaleAsync(Guid carId, Guid guestId, Guid salesPersonId, decimal price);
        Task<bool> FindSaleAsync(Guid carId, Guid guestId, Guid salesPersonId);
        Task<IEnumerable<Sale>> GetAllSalesAsync(Guid salesPersonId);
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(Guid salesPersonId, DateTime start, DateTime end);
    }
}
