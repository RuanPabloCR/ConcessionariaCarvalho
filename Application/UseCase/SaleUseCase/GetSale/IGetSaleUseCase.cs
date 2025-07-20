using Application.Communication.Sales;

namespace Application.UseCase.SaleUseCase.GetSale
{
    public interface IGetSaleUseCase
    {
        Task<IEnumerable<SalesResponse>> ExecuteAsync();
    }
}
