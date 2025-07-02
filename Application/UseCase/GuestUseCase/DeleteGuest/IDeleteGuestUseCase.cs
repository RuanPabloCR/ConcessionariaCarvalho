using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.GuestUseCase.DeleteGuest
{
    public interface IDeleteGuestUseCase
    {
        Task<DeleteGuestResult> DeleteGuestAsync(Guid guestId);
    }
}
