using Domain.Enums;

namespace Application.Communication.Cars
{
    public class CarResponse
    {
        public Guid Id { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public int Year { get; set; }

        public decimal Price { get; set; }
        public CarStatus Status { get; set; }
    }
}
