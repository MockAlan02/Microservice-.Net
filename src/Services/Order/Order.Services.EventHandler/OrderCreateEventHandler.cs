
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Persistence.Context;
using Order.Service.Proxies.Catalog.Interface;
using Order.Services.EventHandler.Command;
using Order.Services.EventHandler.Exceptions;
using static Order.Service.Proxies.Catalog.Commands.ProductInStockUpdateCommand;

namespace Order.Services.EventHandler
{
    public class OrderCreateEventHandler(ApplicationDbContext context,
        ILogger<OrderCreateEventHandler> logger,
        ICatalogProxy catalogProxy) : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<OrderCreateEventHandler> _logger = logger;
        private readonly ICatalogProxy _catalogProxy = catalogProxy;
        public async Task Handle(OrderCreateCommand notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(" --- New order creation started");
            var entry = new Domain.Order();

            //if(notification.ClientId == 0)
            //{
                
            //        _logger.LogError(" --- The ClientId cannot be 0 when creating a product.");
            //        throw new InvalidClientIdException("The ClientId cannot be 0 when creating a product.");
              
            //}

            using var trx = await _context.Database.BeginTransactionAsync(cancellationToken);

            //01. preparing detail
            _logger.LogInformation(" --- Preparing detail");
            PrepareDetail(entry, notification);

            //02. preparing header
            _logger.LogInformation(" --- Preparing header");
            PrepareHeader(entry, notification);

            // 03. Create Order
            _logger.LogInformation(" --- Creating order");
            await _context.Orders.AddAsync(entry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($" --- Order {entry.Id} was created");

            //04. update stocks
            _logger.LogInformation(" ---updating stock");
            try
            {
                await _catalogProxy.UpdateStockAsync(new()
                {
                    Items = notification.Items.Select(x => new ProductInStockItem()
                    {
                        ProductId = x.ProductId,
                        Action = ProductInStockAction.Substract,
                        Stock = x.Quantity
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to create order");
                throw ex;
            }

            await trx.CommitAsync(cancellationToken);

        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.Items = notification.Items.Select(x => new Domain.OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Total = x.Quantity * x.UnitPrice,
                UnitPrice = x.UnitPrice,
            }).ToList();
        }

        public void PrepareHeader(Domain.Order entry, OrderCreateCommand notification)
        {
            entry.ClientId = notification.ClientId;
            entry.PaymentType = notification.PaymentType;
            entry.Status = Common.Enum.OrderStatus.Pending;
            entry.CreateAt = DateTime.UtcNow;

            //Calculate total
            entry.Total = entry.Items.Sum(x => x.Total);
        }
    }
}
