using Microsoft.AspNetCore.Mvc;
using Application.Communication.Guests;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        [HttpPost]
        public ActionResult<GuestsResponse> Register([FromBody] GuestsRequest request)
        {
            // Aqui você faria a chamada para o use case de registro, validação, etc.
            // Exemplo de resposta mockada:
            var response = new GuestsResponse
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Cpf = request.Cpf
            };
            return Ok(response);
        }
    }
}
