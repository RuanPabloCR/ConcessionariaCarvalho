using Microsoft.AspNetCore.Mvc;
using Application.Communication.Guests;
using Application.UseCase.GuestUseCase.RegisterGuest;
using Application.UseCase.GuestUseCase.LoginGuest;
using Infraestructure.JWT;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.RepositoriesInterface;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IRegisterGuestUseCase _registerGuestUseCase;
        private readonly ILoginGuestUseCase _loginGuestUseCase;
        private readonly TokenService _tokenService;
        //private readonly IDeleteGuestRepository _deleteGuestRepository;
        public GuestController(
            IRegisterGuestUseCase registerGuestUseCase,
            ILoginGuestUseCase loginGuestUseCase, /*IDeleteGuestRepository deleteGuestRepository,*/
            TokenService tokenService)
        {
            _registerGuestUseCase = registerGuestUseCase;
            _loginGuestUseCase = loginGuestUseCase;
            //_deleteGuestRepository = deleteGuestRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<SalesPersonWToken>> Register([FromBody] GuestsRequest request)
        {
            var guest = await _registerGuestUseCase.RegisterUserAsync(request);
            if (guest == null)
                return BadRequest("Não foi possível processar sua solicitação");

            var token = _tokenService.Generate(guest);

            var response = new SalesPersonWToken
            {
                Name = guest.Name,
                Email = guest.Email,
                Phone = guest.Phone,
                Cpf = guest.Cpf,
                Token = token
            };
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<SalesPersonWToken>> Login([FromBody] GuestLoginRequest request)
        {
            var guest = await _loginGuestUseCase.LoginAsync(request);
            if (guest == null)
                return Unauthorized("E-mail ou senha inválidos.");

            var token = _tokenService.Generate(guest);

            var response = new SalesPersonWToken
            {
                Name = guest.Name,
                Email = guest.Email,
                Phone = guest.Phone,
                Cpf = guest.Cpf,
                Token = token
            };
            return Ok(response);
        }
        // método pra deletar um usuario, so pra admins
        [HttpDelete("{guestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGuest(Guid guestId,
            [FromServices] IDeleteGuestRepository deleteGuestRepository)
        {
            await deleteGuestRepository.DeleteAsync(guestId);
            return NoContent();
        }

    }
}