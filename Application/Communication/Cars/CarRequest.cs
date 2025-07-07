using Domain.Enums;

namespace Application.Communication.Cars
{
    public class CarRequest
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public CarStatus Status { get; set; }
    }
}
