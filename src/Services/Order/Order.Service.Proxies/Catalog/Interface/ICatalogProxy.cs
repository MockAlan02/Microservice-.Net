using Order.Service.Proxies.Catalog.Commands;

namespace Order.Service.Proxies.Catalog.Interface
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateCommand command);
    }
}
