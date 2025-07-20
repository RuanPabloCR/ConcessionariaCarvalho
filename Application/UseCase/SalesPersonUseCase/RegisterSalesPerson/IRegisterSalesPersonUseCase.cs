using Application.Communication.SalesPeople;
using Domain.Entities;

namespace Application.UseCase.SalesPersonUseCase.RegisterSalesPerson
{
    public interface IRegisterSalesPersonUseCase
    {
        Task<SalesPerson?> RegisterAsync(SalesPersonRequest request);
    }
}
