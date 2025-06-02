using System.Text;
using System.Security.Claims;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.JWT
{
    public class TokenService
    {
        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.SecretKey));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateIdentity(user),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credential,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        private static ClaimsIdentity CreateIdentity(User user) 
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            foreach (var role in user.Roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return ci;
        }
    }
}
