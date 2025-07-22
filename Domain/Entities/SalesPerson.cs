namespace Domain.Entities
{
    public class SalesPerson : User
    {
        public string Phone { get; set; }
        public string Cpf { get; set; }
        public override List<String> Roles { get; } = new List<string> { "SalesPerson", "Guest" };
    }

}
