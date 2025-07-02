using Application.RepositoriesInterface;
using Application.Validators;
using FluentValidation;
using Domain.Entities;
using Application.Communication.Cars;
namespace Application.UseCase.CarUseCase.RegisterCar
{
    public class CreateCarUseCase : ICreateCarUseCase
    {
        private readonly ICreateCarRepository _createCarRepository;
        private readonly IValidator<Car> _validator;
        public  CreateCarUseCase(ICreateCarRepository createCarRepository, IValidator<Car> validator)
        {
            _createCarRepository = createCarRepository;
            _validator = validator;
        }
        public async Task<Car> RegisterCarAsync(CarRequest request)
        {
           var result = _validator.Validate(new Car
            {
                Model = request.Model,
                Brand = request.Brand,
                Year = request.Year,
                Price = request.Price,
                Status = request.Status
            });
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
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
