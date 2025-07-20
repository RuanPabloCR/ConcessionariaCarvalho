using Application.Communication.Sales;

namespace Application.UseCase.SaleUseCase.GetSale
{
    public interface IGetSalesByDateUseCase
    {
        Task<IEnumerable<SalesResponse>> ExecuteAsync(DateTime start, DateTime end);
    }
}
