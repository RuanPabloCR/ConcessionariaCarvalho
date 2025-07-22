using Application.RepositoriesInterface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Infraestructure.JWT
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                throw new InvalidOperationException("User not authenticated.");

            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
                throw new InvalidOperationException("Identification claim not found.");

            // so pra verificar se é o Admin!!
            if (idClaim.Value.Contains("@") && idClaim.Value == "Admin@gmail.com")
            {
                return Guid.Parse("00000000-0000-0000-0000-000000000001");
            }

            if (!Guid.TryParse(idClaim.Value, out var userId))
                throw new InvalidOperationException("Invalid user ID.");

            return userId;
        }
    }
}
