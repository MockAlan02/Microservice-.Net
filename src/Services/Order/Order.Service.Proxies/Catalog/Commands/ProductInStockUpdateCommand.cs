namespace Order.Service.Proxies.Catalog.Commands
{
    public class ProductInStockUpdateCommand
    {
        public IEnumerable<ProductInStockItem>? Items { get; set; } = [];
        public enum ProductInStockAction
        {
            Add,
            Substract
        }
        
        public class ProductInStockItem
        {
            public int ProductId { get; set; }
            public int Stock { get; set; }
            public ProductInStockAction Action { get; set; }
        }
    }
}
