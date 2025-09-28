using ECommerceApi.JJHH17.Data;
using ECommerceApi.JJHH17.Models;

namespace ECommerceApi.JJHH17.Services
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public Product? GetProductById(int id);
        public Product CreateProduct(Product product);
        public Product UpdateProduct(int id, Product product);
        public string? DeleteProduct(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ProductsDbContext _dbContext;

        public ProductService(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product CreateProduct(Product product)
        {
            var savedProduct = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return savedProduct.Entity;
        }

        public string? DeleteProduct(int id)
        {
            Product savedProduct = _dbContext.Products.Find(id);

            if (savedProduct == null) { return null; }

            _dbContext.Products.Remove(savedProduct);
            _dbContext.SaveChanges();

            return $"Successfully deleted product with ID: {id}";
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            Product savedProduct = _dbContext.Products.Find(id);
            return savedProduct;
        }

        public Product UpdateProduct(int id, Product product)
        {
            Product savedProduct = _dbContext.Products.Find(id);

            if (savedProduct == null) { return null; }

            _dbContext.Entry(savedProduct).CurrentValues.SetValues(product);
            _dbContext.SaveChanges();

            return savedProduct;
        }
    }
}
