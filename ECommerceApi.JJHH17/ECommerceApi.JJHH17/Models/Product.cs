namespace ECommerceApi.JJHH17.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
    }
}
