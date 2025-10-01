using ECommerceApi.JJHH17.Models;
using ECommerceApi.JJHH17.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.JJHH17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Example call: http://localhost:5609/api/category/
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var result = _categoryService.GetCategoryById(id);

            if (result == null) { return NotFound(); }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Category> CreateCategory(Category category)
        {
            return Ok(_categoryService.CreateCategory(category));
        }
    }
}
