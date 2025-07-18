using Application.Communication.SalesPerson;
using Application.RepositoriesInterface;
using Domain.Entities;

namespace Application.UseCase.SalesPersonUseCase.LoginSalesPerson
{
    public class LoginSalesPersonUseCase : ILoginSalesPersonUseCase
    {
        private readonly IRegisterSalesPersonRepository _repository;
        public LoginSalesPersonUseCase(IRegisterSalesPersonRepository repository)
        {
            _repository = repository;
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