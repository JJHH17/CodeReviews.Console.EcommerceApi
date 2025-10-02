using ECommerceApi.JJHH17.Data;
using ECommerceApi.JJHH17.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.JJHH17.Services
{
    public interface IProductService
    {
        public List<GetProductsDto> GetAllProducts();
        public Product? GetProductById(int id);
        public Product CreateProduct(CreateProductDto product);
        public Product UpdateProduct(int id, Product updatedProduct);
        public string? DeleteProduct(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ProductsDbContext _dbContext;

        public ProductService(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product CreateProduct(CreateProductDto product)
        {
            if (product == null) 
                throw new ArgumentNullException("Product name is required.",nameof(product));
            
            var newProduct = new Product { ProductName = product.name.Trim(), Price = product.price, CategoryId = product.CategoryId};

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
            return newProduct;
        }

        public string? DeleteProduct(int id)
        {
            Product savedProduct = _dbContext.Products.Find(id);

            if (savedProduct == null) { return null; }

            _dbContext.Products.Remove(savedProduct);
            _dbContext.SaveChanges();

            return $"Successfully deleted product with ID: {id}";
        }

        public List<GetProductsDto> GetAllProducts()
        {
            return _dbContext.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Select(p => new GetProductsDto(
                    p.ProductId,
                    p.ProductName,
                    p.Price,
                    p.CategoryId,
                    p.Category.CategoryName))
                .ToList();
        }

        public Product? GetProductById(int id)
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
