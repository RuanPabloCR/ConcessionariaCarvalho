using Application.Communication.SalesPerson;
using Domain.Entities;
namespace Application.UseCase.SalesPersonUseCase.LoginSalesPerson
{
    public interface ILoginSalesPersonUseCase
    {
        Task<SalesPerson?> LoginAsync(SalesPersonRequest request);
    }
}