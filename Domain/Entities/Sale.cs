namespace Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid GuestId { get; set; }
        public Guid SalesPersonId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Car Car { get; set; }
        public Guest Guest { get; set; }
        public SalesPerson SalesPerson { get; set; }
    }
}
