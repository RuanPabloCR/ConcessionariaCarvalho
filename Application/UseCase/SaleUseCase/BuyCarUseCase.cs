using Application.RepositoriesInterface;
using Application.Services.CarPurshaseService;
namespace Application.UseCase.SaleUseCase
{
    public class BuyCarUseCase : IBuyCarUseCase
    {
        private readonly IGetCarRepository _getCarRepository;
        private readonly IGetGuestRepository _getGuestRepository;
        private readonly IGetSalesPersonRepository _getSalesPersonRepository;
        private readonly IRegisterSaleUseCase _registerSaleUseCase;
        private readonly IUserContext _userContext;
        private readonly ICarPurschase _carPurschaseService;
        public BuyCarUseCase(
            IGetCarRepository getCarRepository,
            IGetGuestRepository getGuestRepository,
            IGetSalesPersonRepository getSalesPersonRepository,
            IRegisterSaleUseCase registerSaleUseCase,
            IUserContext userContext,
            ICarPurschase carPurschase)
        {
            _getCarRepository = getCarRepository;
            _getGuestRepository = getGuestRepository;
            _getSalesPersonRepository = getSalesPersonRepository;
            _registerSaleUseCase = registerSaleUseCase;
            _userContext = userContext;
            _carPurschaseService = carPurschase;
        }

        public async Task<bool> ExecuteAsync(Guid carId)
        {
            var guestId = _userContext.GetUserId();
            var car = await _getCarRepository.GetCarByIdAsync(carId);
            var guest = await _getGuestRepository.GetGuestByIdAsync(guestId);
            var salesPerson = await _getSalesPersonRepository.GetSalesPersonByIdAsync(car.SalesPersonId);

            if (salesPerson == null)
                return false;

            bool validateResult = _carPurschaseService.validateCarPurchase(guest, car, car.Price);
            if (!validateResult)
                return false;

            var result = await _registerSaleUseCase.ExecuteAsync(carId, guestId ,car.SalesPersonId, car.Price);
            return result;
        }
    }
}
