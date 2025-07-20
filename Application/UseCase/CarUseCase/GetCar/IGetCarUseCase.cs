using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.CarUseCase.GetCar
{
    public interface IGetCarUseCase
    {
        Task<IEnumerable<Car>> Search(string searchTerm);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<IEnumerable<Car>> GetCarsByDateAsync(DateOnly date);
    }
}
