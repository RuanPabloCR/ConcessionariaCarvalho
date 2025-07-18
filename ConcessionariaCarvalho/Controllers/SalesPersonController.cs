using Application.Communication.Guests;
using Application.Communication.SalesPerson;
using Application.UseCase.SalesPersonUseCase.DeleteSalesPerson;
using Application.UseCase.SalesPersonUseCase.LoginSalesPerson;
using Application.UseCase.SalesPersonUseCase.RegisterSalesPerson;
using Infraestructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesPersonController : ControllerBase
    {
        private readonly IRegisterSalesPersonUseCase _registerSalesPersonUseCase;
        private readonly ILoginSalesPersonUseCase _loginSalesPersonUseCase;
        private readonly IDeleteSalesPersonUseCase _deleteSalesPersonUseCase;
        private readonly TokenService _tokenService;
        public SalesPersonController(IRegisterSalesPersonUseCase registerSalesPersonUseCase, ILoginSalesPersonUseCase loginSalesPersonUseCase,
            IDeleteSalesPersonUseCase deleteSalesPersonUseCase, TokenService tokenService)
        {
            this._registerSalesPersonUseCase = registerSalesPersonUseCase;
            this._loginSalesPersonUseCase = loginSalesPersonUseCase;
            this._deleteSalesPersonUseCase = deleteSalesPersonUseCase;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] SalesPersonRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid data.");
            }
            await _registerSalesPersonUseCase.RegisterAsync(request);
            return Ok("Salesperson registered successfully");
        }

        [HttpDelete("{salesPersonId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSalesPerson(Guid salesPersonId)
        {
            if (salesPersonId == Guid.Empty)
            {
                return BadRequest("Invalid ID.");
            }
            //Alterar a interface pro método ser do tipo bool
            await _deleteSalesPersonUseCase.DeleteSalesPersonAsync(salesPersonId);
            return NoContent();
        }
        [HttpPost("salesPersonlogin")]
        public async Task<ActionResult<SalesPersonWToken>> Login([FromBody] SalesPersonRequest request)
        {
            var salesPerson = await _loginSalesPersonUseCase.LoginAsync(request);
            if (salesPerson == null)
                return Unauthorized("Invalid information");

            var token = _tokenService.Generate(salesPerson);
            // tenho que verificar se falta atributos no SalesPersonWToken
            var response = new SalesPersonWToken
            {
                Name = salesPerson.Name,
                Email = salesPerson.Email,
                Phone = salesPerson.Phone,
                Cpf = salesPerson.Cpf,
                Token = token
            };
            return Ok(response);
        }
    }
}
