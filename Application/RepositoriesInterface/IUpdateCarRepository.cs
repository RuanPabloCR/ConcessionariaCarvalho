using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.RepositoriesInterface
{
    public interface IUpdateCarRepository
    {
        Task<bool> UpdateCarAsync(Guid id, Car car);
    }
}
