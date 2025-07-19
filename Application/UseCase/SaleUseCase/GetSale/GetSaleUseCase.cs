using Application.RepositoriesInterface;
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
        public async Task<IEnumerable<Sale>> ExecuteAsync()
        {
            var salesPersonId = _userContext.GetUserId();
            if (salesPersonId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            return await _salesRepository.GetAllSalesAsync(salesPersonId);
        }

    }
}