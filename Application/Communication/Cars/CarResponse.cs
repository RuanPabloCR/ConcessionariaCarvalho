using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Communication.Cars
{
    public class CarResponse
    {
        public long Id { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public int Year { get; set; }

        public decimal Price { get; set; }
        public CarStatus Status { get; set; }
    }
}
