using Application.Communication.SalesPeople;
using Domain.Entities;
namespace Application.UseCase.SalesPersonUseCase.LoginSalesPerson
{
    public interface ILoginSalesPersonUseCase
    {
        Task<SalesPerson?> LoginAsync(SalesPersonRequest request);
    }
}