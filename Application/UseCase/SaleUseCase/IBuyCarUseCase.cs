namespace Application.UseCase.SaleUseCase
{
    public interface IBuyCarUseCase
    {
        Task<bool> ExecuteAsync(Guid carId);
    }
}
