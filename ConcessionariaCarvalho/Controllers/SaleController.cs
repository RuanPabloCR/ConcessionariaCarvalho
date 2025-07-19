using Application.UseCase.SaleUseCase.GetSale;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaCarvalho.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IGetSaleUseCase _getSaleUseCase;
        private readonly IGetSalesByDateUseCase _getSaleByIdUseCase;
        public SaleController(IGetSaleUseCase getSaleUseCase, IGetSalesByDateUseCase getSaleByIdUseCase)
        {
            _getSaleUseCase = getSaleUseCase;
            _getSaleByIdUseCase = getSaleByIdUseCase;
        }
        [HttpGet("sales")]
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
    }
}
