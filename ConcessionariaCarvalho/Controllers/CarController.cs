using Application.Communication.Cars;
using Application.UseCase.CarUseCase.RegisterCar;
using Application.UseCase.CarUseCase.UpdateCar;
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
        private readonly UpdateCarUseCase _updateCarUseCase;

        public CarController(CreateCarUseCase createCarUseCase, UpdateCarUseCase updateCarUseCase)
        {
            _createCarUseCase = createCarUseCase;
            _updateCarUseCase = updateCarUseCase;
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

        [HttpPut("update/{id:guid}")]
        [Authorize(Roles = "Admin, SalesPerson")]
        public async Task<IActionResult> UpdateCar([FromRoute] Guid id, [FromBody] CarRequest carRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == Guid.Empty)
            {
                return BadRequest("Error");
            }
            try
            {
                var updatedCar = await _updateCarUseCase.UpdateCarAsync(id, carRequest);
                if (updatedCar == null)
                    return NotFound("Car not found.");

                return Ok(updatedCar);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("status/{id:guid}")]
        [Authorize(Roles = "Admin, SalesPerson")]
        public async Task<IActionResult> UpdateCarStatus([FromRoute] Guid id, [FromBody] UpCarStatusRequest request)
        {
            if (id == Guid.Empty || request == null)
            {
                return BadRequest("Invalid car ID or status request.");
            }
            try
            {
                var updatedCar = await _updateCarUseCase.UpdateCarStatusAsync(id, request);
                if (updatedCar == null)
                    return NotFound("Car not found.");
                return Ok(updatedCar);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}