using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Communication.Sales
{
    public class SalesRequest
    {
       
            public long SalesPersonId { get; set; }  
            public long CarId { get; set; }     
            public DateTime Date  { get; set; } 
            public decimal Value { get; set; }  

    }
}
