using Catalog.Persistence;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Catalog.Common.Enum;
using Catalog.Service.EventHandlers.Exceptions;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateEventHandler(ApplicationDbContext context,
        ILogger<ProductInStockUpdateEventHandler> logger) : INotificationHandler<ProductInStockUpdateStockCommand>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<ProductInStockUpdateEventHandler> _logger = logger;
        public async Task Handle(ProductInStockUpdateStockCommand notificacion, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- ProductInStockUpdateStockCommand started");

            var products = notificacion.Items!.Select(x => x.ProductId);
            var stocks = await _context.ProductInStocks.Where(x => products.Contains(x.ProductId)).ToListAsync(cancellationToken: cancellationToken);

            _logger.LogInformation("--- Retrieve products from database");

            foreach (var item in notificacion.Items!)
            {

                var entry = stocks.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (item.Action == ProductInStockAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock)
                    {
                        _logger.LogError($"Product {entry!.ProductId} - doesn't have enough stock");
                        throw new ProductInStockUpdateStockCommandException($"Product {entry!.ProductId} - doesn't have enough stock");
                    }

                    entry.Stock -= item.Stock;
                    _logger.LogInformation($"--- Product {entry!.ProductId} - its stock was substracted - new stock {entry.Stock}");
                }
                else
                {
                    entry ??= new Domain.ProductInStock
                    {
                        ProductId = item!.ProductId,
                        Stock = item.Stock
                    };


                    if (entry.Id == 0)
                    {
                        await _context.AddAsync(entry);
                        _logger.LogInformation($"--- New stock record was created for - stock {entry.Stock}");
                    }
                    else
                    {
                        entry.Stock += item.Stock;
                        _logger.LogInformation($"--- Product {entry!.Id} - its stock was increased - new stock {entry.Stock}");
                    }

                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
