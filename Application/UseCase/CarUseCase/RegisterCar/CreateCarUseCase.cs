using Application.RepositoriesInterface;
using FluentValidation;
using Domain.Entities;
using Application.Communication.Cars;
namespace Application.UseCase.CarUseCase.RegisterCar
{
    public class CreateCarUseCase : ICreateCarUseCase
    {
        private readonly ICreateCarRepository _createCarRepository;
        private readonly IValidator<CarRequest> _validator;
        private readonly IUserContext _userContext;
        public  CreateCarUseCase(ICreateCarRepository createCarRepository, IValidator<CarRequest> validator,
            IUserContext userContext)
        {
            _createCarRepository = createCarRepository;
            _validator = validator;
            _userContext = userContext;
        }
        public async Task<Car> RegisterCarAsync(CarRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var userId = _userContext.GetUserId();
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Model = request.Model,
                Brand = request.Brand,
                Year = request.Year,
                Price = request.Price,
                Status = request.Status,
                SalesPersonId = userId,
            };
            await _createCarRepository.RegisterCarAsync(car);
            return car;
        }
    }
}
