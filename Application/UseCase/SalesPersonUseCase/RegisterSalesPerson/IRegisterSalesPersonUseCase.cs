using Application.Communication.SalesPerson;
using Domain.Entities;

namespace Application.UseCase.SalesPersonUseCase.RegisterSalesPerson
{
    public interface IRegisterSalesPersonUseCase
    {
        Task<SalesPerson?> RegisterAsync(SalesPersonRequest request);
    }
}
