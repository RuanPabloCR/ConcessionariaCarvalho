using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Communication.Sales
{
    public class SalesResponse
    {
        
            public long SaleId { get; set; }       
            public long VendedorId { get; set; }    
            public string SalesPersonName { get; set; }  
            public long CarId { get; set; }       
            public string CarModel { get; set; }
            public string CarBrand { get; set; }
            public DateTime Date { get; set; }  
            public decimal Value { get; set; }   
    }
}
