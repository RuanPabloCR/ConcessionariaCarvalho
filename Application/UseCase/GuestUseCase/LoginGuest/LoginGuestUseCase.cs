using Application.Communication.Guests;
using Domain.Entities;
using System.Threading.Tasks;
using BCrypt.Net;
using Application.RepositoriesInterface;


namespace Application.UseCase.GuestUseCase.LoginGuest
{
    public class LoginSalesPersonUseCase : ILoginGuestUseCase
    {
        private readonly IRegisterGuestRepository _repository;

        public LoginSalesPersonUseCase(IRegisterGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guest?> LoginAsync(GuestLoginRequest request)
        {
            var guest = await _repository.GetByEmailAsync(request.Email);
            if (guest == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, guest.Password))
                return null;

            return guest;
        }
    }
}