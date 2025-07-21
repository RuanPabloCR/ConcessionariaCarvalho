using Domain.Entities;

namespace Application.Services.CarPurshaseService
{
    public interface ICarPurschase
    {
        public bool validateCarPurchase(Guest guest, Car car, decimal price);
    }
}
