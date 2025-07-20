namespace Application.Communication.SalesPeople
{
    public class SalesPersonRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Cpf { get; set; }
    }
}