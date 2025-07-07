using Application.RepositoriesInterface;

namespace Application.UseCase.SalesPersonUseCase.DeleteSalesPerson
{
    public class DeleteSalesPersonUseCase : IDeleteSalesPersonUseCase
    {
        private readonly IDeleteSalesPersonRepository _deleteSalesPersonRepository;
        public DeleteSalesPersonUseCase(IDeleteSalesPersonRepository deleteSalesPersonRepository)
        {
            _deleteSalesPersonRepository = deleteSalesPersonRepository;
        }
        public async Task<DeleteSalesPersonResult> DeleteSalesPersonAsync(Guid SalesPersonId)
        {
            try
            {
                await _deleteSalesPersonRepository.DeleteAsync(SalesPersonId);
                return DeleteSalesPersonResult.Success;
            }
            catch (KeyNotFoundException)
            {
                return DeleteSalesPersonResult.NotFound;
            }
            catch (Exception)
            {
                return DeleteSalesPersonResult.UnexpectedError;
            }
        }
    }
}
