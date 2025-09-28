using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.JJHH17.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions options) : base(options) { }
    }
}
