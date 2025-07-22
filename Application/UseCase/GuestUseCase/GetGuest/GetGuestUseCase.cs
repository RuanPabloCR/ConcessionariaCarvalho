using Application.RepositoriesInterface;
using Domain.Entities;
namespace Application.UseCase.GuestUseCase.GetGuest
{
    public class GetGuestUseCase : IGetGuestUseCase
    {
        private readonly IGetGuestRepository _repository;
        private readonly IUserContext _userContext;
        public GetGuestUseCase(IGetGuestRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }
        public async Task<Guest?> GetGuestByIdAsync()
        {
            var guestId = _userContext.GetUserId();
            return await _repository.GetGuestByIdAsync(guestId);
        }

    }
}
