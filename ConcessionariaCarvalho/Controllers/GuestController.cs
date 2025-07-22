using Microsoft.AspNetCore.Mvc;
using Application.Communication.Guests;
using Application.UseCase.GuestUseCase.RegisterGuest;
using Application.UseCase.GuestUseCase.LoginGuest;
using Infraestructure.JWT;
using Microsoft.AspNetCore.Authorization;
using Application.RepositoriesInterface;
using Application.UseCase.GuestUseCase.GetGuest;
using System.Security.Claims;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IRegisterGuestUseCase _registerGuestUseCase;
        private readonly ILoginGuestUseCase _loginGuestUseCase;
        private readonly TokenService _tokenService;
        private readonly IGetGuestUseCase _getGuestUseCase;
        private readonly IUserContext _userContext;
        public GuestController(
            IRegisterGuestUseCase registerGuestUseCase,
            ILoginGuestUseCase loginGuestUseCase,
            TokenService tokenService,
            IGetGuestUseCase getGuestUseCase,
            IUserContext userContext)
        {
            _registerGuestUseCase = registerGuestUseCase;
            _loginGuestUseCase = loginGuestUseCase;
            _tokenService = tokenService;
            _getGuestUseCase = getGuestUseCase;
            _userContext = userContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GuestResponseWToken>> Register([FromBody] GuestsRequest request)
        {
            var guest = await _registerGuestUseCase.RegisterUserAsync(request);
            if (guest == null)
                return BadRequest("it wasn't possible to process your request.");

            var token = _tokenService.Generate(guest);

            var response = new GuestResponseWToken
            {
                Name = guest.Name,
                Email = guest.Email,
                Balance = guest.Balance,
                Token = token
            };
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<GuestResponseWToken>> Login([FromBody] GuestLoginRequest request)
        {
            var guest = await _loginGuestUseCase.LoginAsync(request);
            if (guest == null)
                return Unauthorized("Invalid Email or Password");

            var token = _tokenService.Generate(guest);

            var response = new GuestResponseWToken
            {
                Name = guest.Name,
                Email = guest.Email,
                Balance = guest.Balance,
                Token = token
            };
            return Ok(response);
        }

        [HttpDelete("{guestId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGuest(Guid guestId,
            [FromServices] IDeleteGuestRepository deleteGuestRepository)
        {
            await deleteGuestRepository.DeleteAsync(guestId);
            return NoContent();
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GuestsResponse>> GetProfile()
        {
            try
            {
                var guestId = _userContext.GetUserId();
                var guest = await _getGuestUseCase.GetGuestByIdAsync();
                if (guest == null)
                    return NotFound("User not found.");

                var guestResponse = new GuestsResponse
                {
                    Name = guest.Name,
                    Email = guest.Email,
                    Balance = guest.Balance
                };
                return Ok(guestResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

    }
}