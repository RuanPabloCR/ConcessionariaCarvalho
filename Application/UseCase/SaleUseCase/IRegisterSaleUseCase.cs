namespace Application.UseCase.SaleUseCase
{
    public interface IRegisterSaleUseCase
    {
        Task<bool> ExecuteAsync(Guid carId, Guid guestId, Guid salesPersonId, decimal price);
    }
}