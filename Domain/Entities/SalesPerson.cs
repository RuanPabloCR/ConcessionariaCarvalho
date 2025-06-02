using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SalesPerson : User
    {
        public override List<String> Roles { get;} = new List<string> {"salesPerson","guest" };
    }

}
