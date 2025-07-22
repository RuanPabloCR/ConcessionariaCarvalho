using Application.Communication.SalesPeople;
using Application.UseCase.SalesPersonUseCase.DeleteSalesPerson;
using Application.UseCase.SalesPersonUseCase.LoginSalesPerson;
using Application.UseCase.SalesPersonUseCase.RegisterSalesPerson;
using Infraestructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.RepositoriesInterface;
using System.Security.Claims;

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
        private readonly IGetSalesPersonRepository _getSalesPersonRepository;
        private readonly IUserContext _userContext;
        public SalesPersonController(
            IRegisterSalesPersonUseCase registerSalesPersonUseCase,
            ILoginSalesPersonUseCase loginSalesPersonUseCase,
            IDeleteSalesPersonUseCase deleteSalesPersonUseCase,
            TokenService tokenService,
            IGetSalesPersonRepository getSalesPersonRepository,
            IUserContext userContext)
        {
            _registerSalesPersonUseCase = registerSalesPersonUseCase;
            _loginSalesPersonUseCase = loginSalesPersonUseCase;
            _deleteSalesPersonUseCase = deleteSalesPersonUseCase;
            _tokenService = tokenService;
            _getSalesPersonRepository = getSalesPersonRepository;
            _userContext = userContext;
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

            await _deleteSalesPersonUseCase.DeleteSalesPersonAsync(salesPersonId);
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<ActionResult<SalesPersonResponseWToken>> Login([FromBody] SalesPersonRequest request)
        {
            var salesPerson = await _loginSalesPersonUseCase.LoginAsync(request);
            if (salesPerson == null)
                return Unauthorized("Invalid information");

            var token = _tokenService.Generate(salesPerson);

            var response = new SalesPersonResponseWToken
            {
                Name = salesPerson.Name,
                Email = salesPerson.Email,
                Phone = salesPerson.Phone,
                Cpf = salesPerson.Cpf,
                Token = token
            };
            return Ok(response);
        }

        [HttpGet("profile")]
        [Authorize(Roles = "SalesPerson")]
        public async Task<ActionResult<SalesPersonResponse>> GetProfile()
        {
            try
            {
                var salesPersonId = _userContext.GetUserId();
                var salesPerson = await _getSalesPersonRepository.GetSalesPersonByIdAsync(salesPersonId);
                if (salesPerson == null)
                    return NotFound("SalesPerson not found.");

                var response = new SalesPersonResponse
                {
                    Name = salesPerson.Name,
                    Email = salesPerson.Email,
                    Phone = salesPerson.Phone,
                    Cpf = salesPerson.Cpf
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
