using ECommerceApi.JJHH17.Data;
using ECommerceApi.JJHH17.Models;

namespace ECommerceApi.JJHH17.Services
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategories();
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

        public List<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
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
