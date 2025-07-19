using Application.RepositoriesInterface;
using Domain.Entities;

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
        public async Task<IEnumerable<Sale>> ExecuteAsync(DateTime start, DateTime end)
        {
            var salesPersonId = _userContext.GetUserId();
            if (salesPersonId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            return await _salesRepository.GetSalesByDateRangeAsync(salesPersonId, start, end);
        }
    }
}
