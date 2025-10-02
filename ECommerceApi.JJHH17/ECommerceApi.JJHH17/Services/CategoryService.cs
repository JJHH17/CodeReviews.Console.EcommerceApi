using ECommerceApi.JJHH17.Data;
using ECommerceApi.JJHH17.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.JJHH17.Services
{
    public interface ICategoryService
    {
        public List<CategoryWithProductsDto> GetAllCategories();
        public Category GetCategoryById(int id);
        public Category CreateCategory(CreateCategoryDto category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ProductsDbContext _dbContext;

        public CategoryService(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CategoryWithProductsDto> GetAllCategories()
        {
            return _dbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.CategoryName)
                .Select(c => new CategoryWithProductsDto(
                    c.CategoryId,
                    c.CategoryName,
                    c.Products
                        .OrderBy(p => p.ProductName)
                        .Select(p => new ProductDto(p.ProductId, p.ProductName, p.Price))
                        .ToList()
                        ))
                .ToList();
        }

        public Category? GetCategoryById(int id)
        {
            Category savedCategory = _dbContext.Categories.Find(id);
            return savedCategory;
        }

        public Category CreateCategory(CreateCategoryDto category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required.", nameof(category));

            bool exists = _dbContext.Categories.Any(c => c.CategoryName == category.Name);
            if (exists)
                throw new InvalidOperationException($"Category {category.Name} already exists");

            var newCategory = new Category { CategoryName = category.Name.Trim() };

            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();
            return newCategory;
        }
    }
}
