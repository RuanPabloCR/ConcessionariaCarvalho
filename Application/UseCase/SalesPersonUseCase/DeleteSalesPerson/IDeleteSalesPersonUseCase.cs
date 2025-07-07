namespace Application.UseCase.SalesPersonUseCase.DeleteSalesPerson
{
    public interface IDeleteSalesPersonUseCase
    {
        Task<DeleteSalesPersonResult> DeleteSalesPersonAsync(Guid salesPersonId);
    }
}
