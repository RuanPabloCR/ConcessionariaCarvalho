using Application.Communication.SalesPerson;
using Application.RepositoriesInterface;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCase.SalesPersonUseCase.RegisterSalesPerson
{
    public class RegisterSalesPersonUseCase : IRegisterSalesPersonUseCase
    {
        private readonly IRegisterSalesPersonRepository _repository;
        private readonly IValidator<SalesPersonRequest> _validator;
        public RegisterSalesPersonUseCase(IRegisterSalesPersonRepository repository, IValidator<SalesPersonRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<SalesPerson?> RegisterAsync(SalesPersonRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var existingSalesPerson = await _repository.GetByEmailAsync(request.Email);
            if (existingSalesPerson != null)
            {
                throw new InvalidOperationException("A sales person with this email already exists.");
            }
            var salesPerson = new SalesPerson
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            await _repository.AddAsync(salesPerson);
            return salesPerson;
        }
    }
}
