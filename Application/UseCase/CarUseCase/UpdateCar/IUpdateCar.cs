using Application.Communication.Cars;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.CarUseCase.UpdateCar
{
    public interface IUpdateCar
    {
        Task<CarResponse?> UpdateCarAsync(Guid id, CarRequest carRequest);
    }
}
