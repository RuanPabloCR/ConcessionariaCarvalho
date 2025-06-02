using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public long GuestId { get; set; }
        public long SalesPersonId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
