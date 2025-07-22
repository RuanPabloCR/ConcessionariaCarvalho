using Domain.Entities;
namespace Application.UseCase.GuestUseCase.GetGuest
{
    public interface IGetGuestUseCase
    {
        Task<Guest?> GetGuestByIdAsync();
    }
}
