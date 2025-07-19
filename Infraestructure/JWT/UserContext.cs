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
                throw new InvalidOperationException("Usuário não autenticado.");

            // Supondo que o SalesPersonId está na claim "sub" ou "id"
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub") ?? user.FindFirst("id");
            if (idClaim == null)
                throw new InvalidOperationException("Claim de identificação não encontrada.");

            return Guid.Parse(idClaim.Value);
        }
    }
}
