using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Data;
namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminAuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Application.Services.PasswordEncryption.IPasswordEncryptionService _passwordEncryptionService;
        private readonly Infraestructure.JWT.TokenService _tokenService;

        public AdminAuthController(AppDbContext context, Application.Services.PasswordEncryption.IPasswordEncryptionService passwordEncryptionService, Infraestructure.JWT.TokenService tokenService)
        {
            _context = context;
            _passwordEncryptionService = passwordEncryptionService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginRequest request)
        {

            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (admin == null)
                return Unauthorized("Usuário ou senha inválidos.");

            if (!_passwordEncryptionService.VerifyPassword(request.Password, admin.Password))
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _tokenService.GenerateForAdmin(admin);

            return Ok(new
            {
                token,
                email = admin.Email
            });
        }
    }

    public class AdminLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
