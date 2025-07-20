namespace Application.Communication.Sales
{
    public class SalesResponse
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public int CarYear { get; set; }
        public Guid GuestId { get; set; }
        public string GuestName { get; set; }
        public Guid SalesPersonId { get; set; }
        public string SalesPersonName { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
