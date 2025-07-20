using Application.Communication.Sales;
using Application.UseCase.SaleUseCase;
using Application.UseCase.SaleUseCase.GetSale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IGetSaleUseCase _getSaleUseCase;
        private readonly IGetSalesByDateUseCase _getSaleByIdUseCase;
        private readonly IRegisterSaleUseCase _registerSaleUseCase;
        public SaleController(IGetSaleUseCase getSaleUseCase, IGetSalesByDateUseCase getSaleByIdUseCase,
            IRegisterSaleUseCase registerSaleUseCase)
        {
            _getSaleUseCase = getSaleUseCase;
            _getSaleByIdUseCase = getSaleByIdUseCase;
            _registerSaleUseCase = registerSaleUseCase;
        }
        [HttpGet("sales")]
        [Authorize(Roles = "Admin, SalesPerson")]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _getSaleUseCase.ExecuteAsync();
            if (sales == null || !sales.Any())
            {
                return NotFound("No sales found.");
            }
            return Ok(sales);
        }
        [HttpGet("sales-by-date")]
        [Authorize(Roles = "Admin, SalesPerson")]
        public async Task<IActionResult> GetSalesByDate([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if (start > end)
            {
                return BadRequest("Start date cannot be after end date.");
            }
            var sales = await _getSaleByIdUseCase.ExecuteAsync(start, end);
            if (sales == null || !sales.Any())
            {
                return NotFound("No sales found for the specified date range.");
            }
            return Ok(sales);
        }
        [HttpPost("register")]
        [Authorize(Roles = "Admin, SalesPerson")]
        public async Task<IActionResult> RegisterSale([FromBody] SalesRequest request)
        {
            if (request == null || request.CarId == Guid.Empty || request.GuestId == Guid.Empty ||
                request.SalesPersonId == Guid.Empty || request.Price <= 0)
            {
                return BadRequest("Invalid sale data provided.");
            }
            var result = await _registerSaleUseCase.ExecuteAsync(request.CarId, request.GuestId, request.SalesPersonId, request.Price);
            if (!result)
            {
                return StatusCode(500, "An error occurred while registering the sale.");
            }
            return Ok("Sale registered successfully.");
        }
    }
}
