using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Guest : User
    {
        public string Phone { get; set; }
        public string Cpf { get; set; }
        public override List<String> Roles { get; } = new List<string> { "guest" };
    }
}
