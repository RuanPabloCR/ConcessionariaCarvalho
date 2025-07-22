using Application.Communication.Cars;
using Application.UseCase.CarUseCase.GetCar;
using Application.UseCase.CarUseCase.RegisterCar;
using Application.UseCase.CarUseCase.UpdateCar;
using Application.UseCase.SaleUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICreateCarUseCase _createCarUseCase;
        private readonly IUpdateCarUseCase _updateCarUseCase;
        private readonly IGetCarUseCase _getCarUseCase;
        private readonly IBuyCarUseCase _buyCarUseCase;
        public CarController(ICreateCarUseCase createCarUseCase, IUpdateCarUseCase updateCarUseCase,
            IGetCarUseCase getCarUseCase,
            IBuyCarUseCase buyCarUseCase)
        {
            _createCarUseCase = createCarUseCase;
            _updateCarUseCase = updateCarUseCase;
            _getCarUseCase = getCarUseCase;
            _buyCarUseCase = buyCarUseCase;
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
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var cars = await _getCarUseCase.GetAllCarsAsync();
                if (cars == null || !cars.Any())
                {
                    return NotFound("No cars found.");
                }
                var response = cars.Select(c => new CarResponse
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    Year = c.Year,
                    Price = c.Price,
                    Status = c.Status
                });
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchCars([FromQuery, Required] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }
            try
            {
                var cars = await _getCarUseCase.Search(searchTerm);
                if (cars == null || !cars.Any())
                {
                    return NotFound("No cars found matching the search term.");
                }
                var response = cars.Select(c => new CarResponse
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    Year = c.Year,
                    Price = c.Price,
                    Status = c.Status
                });
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("date/{date:datetime}")]
        public async Task<IActionResult> GetCarsByDate([FromRoute] DateTime date)
        {
            try
            {
                var cars = await _getCarUseCase.GetCarsByDateAsync(DateOnly.FromDateTime(date));
                if (cars == null || !cars.Any())
                {
                    return NotFound("No cars found for the specified date.");
                }
                var response = cars.Select(c => new CarResponse
                {
                    Id = c.Id,
                    Model = c.Model,
                    Brand = c.Brand,
                    Year = c.Year,
                    Price = c.Price,
                    Status = c.Status
                });
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("buy/{carId:guid}")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> BuyCar([FromRoute] Guid carId)
        {
            if (carId == Guid.Empty)
            {
                return BadRequest("Invalid car ID.");
            }
            try
            {
                var result = await _buyCarUseCase.ExecuteAsync(carId);
                if (!result)
                {
                    return BadRequest("Car purchase failed. Please check the car availability or your account status.");
                }
                return Ok("Car purchased successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}