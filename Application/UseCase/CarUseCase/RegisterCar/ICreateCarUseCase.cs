using Application.Communication.Cars;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.CarUseCase.RegisterCar
{
    internal interface ICreateCarUseCase
    {
        Task<Car?> RegisterCarAsync(CarRequest carRequest);
    }
}
