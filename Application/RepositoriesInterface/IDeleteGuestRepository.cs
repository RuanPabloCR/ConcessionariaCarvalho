using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RepositoriesInterface
{
    public interface IDeleteGuestRepository
    {
        Task DeleteAsync(Guid guestId);
    }
}
