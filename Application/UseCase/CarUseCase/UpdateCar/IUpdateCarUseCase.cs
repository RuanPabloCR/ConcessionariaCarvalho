using Application.Communication.Cars;

namespace Application.UseCase.CarUseCase.UpdateCar
{
    public interface IUpdateCarUseCase
    {
        Task<CarResponse?> UpdateCarAsync(Guid id, CarRequest carRequest);
        Task<CarResponse?> UpdateCarStatusAsync(Guid id, UpCarStatusRequest request);
    }
}
