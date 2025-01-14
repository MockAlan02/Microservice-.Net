using Catalog.Service.EventHandlers;
using Catalog.Test.Config;
using Microsoft.Extensions.Logging;
using Moq;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Common.Enum;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Domain;

namespace Catalog.Test
{
    [TestClass]
    public class ProductInStockUpdateEventHandlerTest
    {
        private ILogger<ProductInStockUpdateEventHandler> GetLogger
        {
            get
            {
                return new Mock<ILogger<ProductInStockUpdateEventHandler>>()
                    .Object;
            }
        }


        [TestMethod]
        public async Task TrySubstractWhenProductHasStock()
        {
            var context = ApplicationDbContextInMemory.Get();

            var productInStockId = 1;
            var productId = 1;

            context.ProductInStocks.Add(new Domain.ProductInStock()
            {
                Id = productInStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateEventHandler(context, GetLogger);

            await handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items =
                [
                   new() {
                        ProductId = productId,
                       Stock = 1,
                       Action = ProductInStockAction.Substract
                   }
                ]
            }, new CancellationToken());

        }
        
        [TestMethod]
        [ExpectedException(typeof(ProductInStockUpdateStockCommandException))]
        public async Task TrySubstractWhenProductHasntStock()
        {
            var context = ApplicationDbContextInMemory.Get();

            var productInStockId = 2;
            var productId = 2;

            context.ProductInStocks.Add(new Domain.ProductInStock()
            {
                Id = productInStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateEventHandler(context, GetLogger);

            await handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items =
                [
                   new() {
                        ProductId = productId,
                       Stock = 2,
                       Action = ProductInStockAction.Substract
                   }
                ]
            }, new CancellationToken());

        }

        [TestMethod]
        public void TryAddStockWhenProductExist()
        {
            var context = ApplicationDbContextInMemory.Get();

            var productInStockId = 3;
            var productId = 3;

            context.ProductInStocks.Add(new ProductInStock()
            {
                Id = productInStockId,
                ProductId = productId,
                Stock = 1
            });

            context.SaveChanges();

            var handler = new ProductInStockUpdateEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items =
                [
                   new() {
                       ProductId = productId,
                       Stock = 2,
                       Action = ProductInStockAction.Add
                   }
                ]
            }, new CancellationToken()).Wait();

            var stokcInDB = context!.ProductInStocks.SingleOrDefault(x => x.ProductId == productId)!.Stock;
            Assert.AreEqual(stokcInDB, 3);
        }

        [TestMethod]
        public void TryAddStockWhenProductNotExist()
        {
            var context = ApplicationDbContextInMemory.Get();
            var productId = 4;

            var handler = new ProductInStockUpdateEventHandler(context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items =
                [
                   new() {
                       ProductId = productId,
                       Stock = 2,
                       Action = ProductInStockAction.Add
                   }
                ]
            }, new CancellationToken()).Wait();

            var stokcInDB = context!.ProductInStocks.SingleOrDefault(x => x.ProductId == productId)!.Stock;
            Assert.AreEqual(stokcInDB, 2);
        }
    }
}