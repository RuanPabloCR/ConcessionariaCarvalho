namespace Domain.Entities
{
    public class Guest : User
    {
        public string Phone { get; set; }
        public string Cpf { get; set; }
        public override List<String> Roles { get; } = new List<string> { "guest" };
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Balance { get; set; } = 0;

    }
}
