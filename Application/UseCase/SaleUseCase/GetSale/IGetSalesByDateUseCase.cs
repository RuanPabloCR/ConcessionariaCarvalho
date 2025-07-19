using Domain.Entities;

namespace Application.UseCase.SaleUseCase.GetSale
{
    public interface IGetSalesByDateUseCase
    {
        Task<IEnumerable<Sale>> ExecuteAsync(DateTime start, DateTime end);
    }
}
