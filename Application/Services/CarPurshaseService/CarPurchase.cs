using Domain.Entities;
using Domain.Enums;

namespace Application.Services.CarPurshaseService
{
    public class CarPurchase : ICarPurschase
    {
        public bool validateCarPurchase(Guest guest, Car car, decimal price)
        {
            if (car == null || car.Status != CarStatus.Available)
                return false;
            if( guest == null)
                return false;
            if(price <= 0 || price != car.Price)
                return false;
            if(guest.Balance < price)
                return false;
            return true;
        }
    }
}
