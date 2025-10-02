using ECommerceApi.JJHH17.Data;
using ECommerceApi.JJHH17.Models;
using ECommerceApi.JJHH17.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.JJHH17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Example call: http://localhost:5609/api/product/
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<GetProductsDto>> GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var result = _productService.GetProductById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<CreateProductDto> CreateProduct(CreateProductDto product)
        {
            return Ok(_productService.CreateProduct(product));
        }

        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, Product updatedProduct)
        {
            var result = _productService.UpdateProduct(id, updatedProduct);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }
    }
}
