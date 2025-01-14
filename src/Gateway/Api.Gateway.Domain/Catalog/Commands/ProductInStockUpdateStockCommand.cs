using Api.Gateway.Domain.Catalog.Common.Enum;

namespace Api.Gateway.Domain.Catalog.Commands
{
    public class ProductInStockUpdateStockCommand 
    {
        public IEnumerable<ProductInStockItem>? Items { get; set; } = [];
    }


    public class ProductInStockItem
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public ProductInStockAction Action { get; set; }
    }
}
