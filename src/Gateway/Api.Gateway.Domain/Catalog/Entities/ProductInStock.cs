namespace Api.Gateway.Domain.Catalog.Entities
{
    public class ProductInStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }

    }
}
