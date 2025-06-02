using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Communication.Cars
{
    public class CarRequest
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public CarStatus Status { get; set; }
    }
}
