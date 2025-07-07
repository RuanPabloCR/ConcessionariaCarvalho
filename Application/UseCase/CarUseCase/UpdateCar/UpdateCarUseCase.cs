
using Application.Communication.Cars;
using Application.RepositoriesInterface;
using Domain.Entities;
using FluentValidation;
namespace Application.UseCase.CarUseCase.UpdateCar
{
    public class UpdateCarUseCase : IUpdateCarUseCase
    {
        private readonly IUpdateCarRepository _updateCarRepository;
        private readonly IValidator<CarRequest> _carRequestValidator;
        public UpdateCarUseCase(IUpdateCarRepository updatecar, IValidator<CarRequest> carRequestValidator)
        {
            _updateCarRepository = updatecar;
            _carRequestValidator = carRequestValidator;
        }
        public async Task<CarResponse?> UpdateCarAsync(Guid id, CarRequest carRequest)
        {
            var validationResult = await _carRequestValidator.ValidateAsync(carRequest);
            if(!validationResult.IsValid) {
                return null;
            }

            var car = new Car
            {
                Id = id,
                Model = carRequest.Model,
                Brand = carRequest.Brand,
                Year = carRequest.Year,
                Price = carRequest.Price,
                Status = carRequest.Status
            };

            var updated = await _updateCarRepository.UpdateCarAsync(id, car);
            if (!updated)
            {
                return null;
            }
            return new CarResponse
            {
                Id = id,
                Model = car.Model,
                Brand = car.Brand,
                Year = car.Year,
                Price = car.Price,
                Status = car.Status
            };
        }

        public async Task<CarResponse?> UpdateCarStatusAsync(Guid id, UpCarStatusRequest request)
        {
            var car = await _updateCarRepository.GetCarByIdAsync(id);
            if (car == null)
            {
                return null;
            }
            car.Status = request.status;
            var updated = await _updateCarRepository.UpdateCarAsync(id, car);
            if (!updated)
            {
                return null;
            }
            return new CarResponse
            {
                Id = id,
                Model = car.Model,
                Brand = car.Brand,
                Year = car.Year,
                Price = car.Price,
                Status = car.Status
            };
        }
    }
}
