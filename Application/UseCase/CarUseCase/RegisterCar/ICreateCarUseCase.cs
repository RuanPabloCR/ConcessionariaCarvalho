using Application.Communication.Cars;
using Domain.Entities;

namespace Application.UseCase.CarUseCase.RegisterCar
{
    public interface ICreateCarUseCase
    {
        Task<Car?> RegisterCarAsync(CarRequest carRequest);
    }
}
