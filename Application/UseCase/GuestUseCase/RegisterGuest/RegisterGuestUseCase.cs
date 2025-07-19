using Application.Communication.Guests;
using Application.RepositoriesInterface;
using Domain.Entities;
using FluentValidation;


namespace Application.UseCase.GuestUseCase.RegisterGuest
{
    public class RegisterGuestUseCase : IRegisterGuestUseCase
    {
        private readonly IValidator<GuestsRequest> _validator;
        private readonly IRegisterGuestRepository _repository;
        public RegisterGuestUseCase(IValidator<GuestsRequest> validator, IRegisterGuestRepository registerGuestRepository)
        {
            _validator = validator;
            _repository = registerGuestRepository;
        }
        public async Task<Guest> RegisterUserAsync(GuestsRequest guestsRequest)
        {

            var validationResult = await _validator.ValidateAsync(guestsRequest);
            if (!validationResult.IsValid)
                return null;

            var existing = await _repository.GetByEmailAsync(guestsRequest.Email);
            if (existing != null)
                return null;

            var guest = new Guest
            {
                Name = guestsRequest.Name,
                Email = guestsRequest.Email,
                Phone = guestsRequest.Phone,
                Cpf = guestsRequest.Cpf,
                Password = BCrypt.Net.BCrypt.HashPassword(guestsRequest.Password)
            };

            await _repository.AddAsync(guest);
            return guest;
        }
    }
}
