using Application.Communication.Guests;
using Domain.Entities;
namespace Application.UseCase.GuestUseCase.RegisterGuest
{
    public interface IRegisterGuestUseCase
    {
        Task<Guest?> RegisterUserAsync(GuestsRequest guestRequest);
    }
}