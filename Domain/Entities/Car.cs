using Domain.Enums;

namespace Domain.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
       
        public string Model { get; set; }= string.Empty;
       
        public string Brand { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public CarStatus Status { get; set; }
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    }
}
