using Application.Communication.Guests;
using Application.RepositoriesInterface;
using Domain.Entities;
using FluentValidation;


namespace Application.UseCase.GuestUseCase.RegisterGuest
{
    public class RegisterGuestUseCase : IRegisterGuestUseCase
    {
        private readonly IValidator<Guest> _validator;
        private readonly IRegisterGuestRepository _repository;
        public RegisterGuestUseCase(IValidator<Guest> validator, IRegisterGuestRepository registerGuestRepository)
        {
            _validator = validator;
            _repository = registerGuestRepository;
        }
        public async Task<Guest> RegisterUserAsync(GuestsRequest guestsRequest)
        {
            
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

            // Validação
            var validationResult = await _validator.ValidateAsync(guest);
            if (!validationResult.IsValid)
                return null;
            // salar o usuario no banco de dados
            await _repository.AddAsync(guest);
            return guest;
        }
    }
}
