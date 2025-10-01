using ECommerceApi.JJHH17.Data;
using ECommerceApi.JJHH17.Models;

namespace ECommerceApi.JJHH17.Services
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategories();
        public Category GetCategoryById(int id);
        public Category CreateCategory(Category category);
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

        public Category CreateCategory(Category category)
        {
            var savedCategory = _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return savedCategory.Entity;
        }
    }
}
