using Domain.Entities;

namespace Application.UseCase.SaleUseCase.GetSale
{
    public interface IGetSaleUseCase
    {
        Task<IEnumerable<Sale>> ExecuteAsync();
    }
}
