using Application.RepositoriesInterface;
using Application.Communication.Sales;
using Domain.Entities;
namespace Application.UseCase.SaleUseCase.GetSale
{
    public class GetSaleUseCase : IGetSaleUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IUserContext _userContext;
        public GetSaleUseCase(ISalesRepository salesRepository, IUserContext userContext)
        {
            _salesRepository = salesRepository;
            _userContext = userContext;
        }
        public async Task<IEnumerable<SalesResponse>> ExecuteAsync()
        {
            var salesPersonId = _userContext.GetUserId();
            if (salesPersonId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var sales = await _salesRepository.GetAllSalesAsync(salesPersonId);
            return sales.Select(s => new SalesResponse
            {
                Id = s.Id,
                CarId = s.CarId,
                CarModel = s.Car?.Model ?? string.Empty,
                CarBrand = s.Car?.Brand ?? string.Empty,
                CarYear = s.Car?.Year ?? 0,
                GuestId = s.GuestId,
                GuestName = s.Guest?.Name ?? string.Empty,
                SalesPersonId = s.SalesPersonId,
                SalesPersonName = s.SalesPerson?.Name ?? string.Empty,
                Price = s.Price,
                Date = s.Date
            });
        }
    }
}