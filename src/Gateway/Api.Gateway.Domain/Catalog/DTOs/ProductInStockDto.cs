namespace Api.Gateway.Domain.Catalog.DTOs
{
    public class ProductInStockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }

    }
}
