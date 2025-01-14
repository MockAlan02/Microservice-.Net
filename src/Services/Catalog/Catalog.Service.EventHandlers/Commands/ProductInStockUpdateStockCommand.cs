using Catalog.Common.Enum;
using MediatR;

namespace Catalog.Service.EventHandlers.Commands
{
    public class ProductInStockUpdateStockCommand : INotification
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
