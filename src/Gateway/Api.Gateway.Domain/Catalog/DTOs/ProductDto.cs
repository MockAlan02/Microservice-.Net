namespace Api.Gateway.Domain.Catalog.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        //Navigation Property
        public ProductInStockDto? Stock { get; set; }
    }
}
