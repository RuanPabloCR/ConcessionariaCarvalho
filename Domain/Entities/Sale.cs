using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid GuestId { get; set; }
        public Guid SalesPersonId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
