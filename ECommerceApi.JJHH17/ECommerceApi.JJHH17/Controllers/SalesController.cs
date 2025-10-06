using ECommerceApi.JJHH17.Models;
using ECommerceApi.JJHH17.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ECommerceApi.JJHH17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Example call: http://localhost:5609/api/sale/
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public ActionResult<List<GetProductsDto>> GetAllSales()
        {
            return Ok(_saleService.GetAllSales());
        }

        [HttpGet("{id}")]
        public ActionResult<Sale> GetSaleById(int id)
        {
            var result = _saleService.GetSaleById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<SaleWithProductsDto> CreateSale([FromBody] CreateSaleDto sale)
        {
            try
            {
                var created = _saleService.CreateSale(sale);

                var dto = new SaleWithProductsDto(
                    created.SaleId,
                    created.ItemCount,
                    created.SalePrice,
                    created.Products
                        .OrderBy(p => p.ProductName)
                        .Select(p => new ProductDto(p.ProductId, p.ProductName, p.Price))
                        .ToList()
                    );

                return CreatedAtAction(nameof(GetSaleById), new { id = created.SaleId }, dto);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
