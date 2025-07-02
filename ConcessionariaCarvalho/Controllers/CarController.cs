using Application.Communication.Cars;
using Application.UseCase.CarUseCase.RegisterCar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CreateCarUseCase _createCarUseCase;
        public CarController(CreateCarUseCase createCarUseCase)
        {
            _createCarUseCase = createCarUseCase;
        }
        [HttpPost("register")]
        [Authorize(Roles = "Admin, SalesPerson")]
        public async Task<IActionResult> RegisterCar([FromBody] CarRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid car data.");
            }
            try
            {
                var car = await _createCarUseCase.RegisterCarAsync(request);
                var response = new CarResponse
                {
                    Id = car.Id,
                    Model = car.Model,
                    Brand = car.Brand,
                    Year = car.Year,
                    Price = car.Price,
                    Status = car.Status
                };
                return CreatedAtAction(nameof(RegisterCar), new { id = response.Id }, response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}