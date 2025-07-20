using Application.RepositoriesInterface;
using Application.Communication.Sales;

namespace Application.UseCase.SaleUseCase.GetSale
{
    public class GetSalesByDateUseCase : IGetSalesByDateUseCase
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IUserContext _userContext;
        public GetSalesByDateUseCase(ISalesRepository salesRepository, IUserContext userContext)
        {
            _salesRepository = salesRepository;
            _userContext = userContext;
        }
        public async Task<IEnumerable<SalesResponse>> ExecuteAsync(DateTime start, DateTime end)
        {
            var salesPersonId = _userContext.GetUserId();
            if (salesPersonId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var sales = await _salesRepository.GetSalesByDateRangeAsync(salesPersonId, start, end);
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
