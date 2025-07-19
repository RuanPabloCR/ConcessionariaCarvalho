using Application.RepositoriesInterface;

namespace Application.UseCase.SaleUseCase
{
    public class RegisterSaleUseCase : IRegisterSaleUseCase
    {
        private readonly ISalesRepository _registerSaleRepository;

        public RegisterSaleUseCase(ISalesRepository registerSaleRepository)
        {
            _registerSaleRepository = registerSaleRepository;
        }

        public async Task<bool> ExecuteAsync(Guid carId, Guid guestId, Guid salesPersonId, decimal price)
        {
            return await _registerSaleRepository.RegisterSaleAsync(carId, guestId, salesPersonId, price);
        }
    }
}
