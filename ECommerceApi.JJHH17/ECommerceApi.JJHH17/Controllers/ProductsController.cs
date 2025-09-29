using ECommerceApi.JJHH17.Models;
using ECommerceApi.JJHH17.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.JJHH17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Example route = http://localhost:5609/api/product/
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Endpoints part of course
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            return Ok(_productService.GetProductById(id));
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            return Ok(_productService.CreateProduct(product));
        }

        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, Product updatedProduct)
        {
            return Ok(_productService.UpdateProduct(id, updatedProduct));
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteProduct(int id)
        {
            return Ok(_productService.DeleteProduct(id));
        }
    }
}
