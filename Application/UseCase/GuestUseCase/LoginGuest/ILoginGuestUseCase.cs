using Application.Communication.Guests;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.UseCase.GuestUseCase.LoginGuest
{
    public interface ILoginGuestUseCase
    {
        Task<Guest?> LoginAsync(GuestLoginRequest request);
    }
}