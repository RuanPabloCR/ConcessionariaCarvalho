using Application.RepositoriesInterface;

namespace Application.UseCase.SaleUseCase
{
    public class RegisterSaleUseCase : IRegisterSaleUseCase
    {
        private readonly IRegisterSale _registerSaleRepository;

        public RegisterSaleUseCase(IRegisterSale registerSaleRepository)
        {
            _registerSaleRepository = registerSaleRepository;
        }

        public async Task<bool> ExecuteAsync(Guid carId, Guid guestId, Guid salesPersonId, decimal price)
        {
            return await _registerSaleRepository.RegisterSaleAsync(carId, guestId, salesPersonId, price);
        }
    }
}
