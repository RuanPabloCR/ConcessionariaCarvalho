using Application.Communication.SalesPerson;
using Application.RepositoriesInterface;
using Domain.Entities;
using FluentValidation;

namespace Application.UseCase.SalesPersonUseCase.LoginSalesPerson
{
    public class LoginSalesPersonUseCase : ILoginSalesPersonUseCase
    {
        private readonly IRegisterSalesPersonRepository _repository;
        private readonly IValidator<SalesPersonRequest> _validator;
        public LoginSalesPersonUseCase(IRegisterSalesPersonRepository repository, IValidator<SalesPersonRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<SalesPerson?> LoginAsync(SalesPersonRequest request)
        {

            var salesPerson = await _repository.GetByEmailAsync(request.Email);
            if (salesPerson == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, salesPerson.Password))
                return null;

            return salesPerson;
        }
    }
}