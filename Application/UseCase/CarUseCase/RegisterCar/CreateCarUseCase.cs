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
        public  CreateCarUseCase(ICreateCarRepository createCarRepository, IValidator<CarRequest> validator)
        {
            _createCarRepository = createCarRepository;
            _validator = validator;
        }
        public async Task<Car> RegisterCarAsync(CarRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var car = new Car
            {
                Id = Guid.NewGuid(),
                Model = request.Model,
                Brand = request.Brand,
                Year = request.Year,
                Price = request.Price,
                Status = request.Status
            };
            await _createCarRepository.RegisterCarAsync(car);
            return car;
        }
    }
}
