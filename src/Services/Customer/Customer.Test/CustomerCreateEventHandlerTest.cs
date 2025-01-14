using Castle.Core.Logging;
using Customer.Persistence.Context;
using Customer.Services.EventHandler;
using Customer.Services.EventHandler.Exceptions;
using Customer.Services.Queries;
using Customer.Test.Config;
using Microsoft.Extensions.Logging;
using Moq;
using System.Runtime.CompilerServices;
namespace Customer.Test
{
    [TestClass]
    public class CustomerCreateEventHandlerTest
    {

        private ILogger<CustomerCreateEventHandler> _logger
        {
            get
            {
                return new Mock<ILogger<CustomerCreateEventHandler>>()
                    .Object;
            }
        }


        [TestMethod]
        [ExpectedException(typeof(CustomerCreateEventHandlerException))]
        public async Task TryCreateUserWithoutText()
        {
            var context = ConfigurationContext.GetContext();
            var name = "";

            var handler = new CustomerCreateEventHandler(context, _logger);
            await handler.Handle(new Services.EventHandler.Command.CustomerCreateCommand
            {
                Name = name
            }, new CancellationToken());

        }


        [TestMethod]
        public async Task TryGetUnexistedUser()
        {
            var context = ConfigurationContext.GetContext();

            var handler = new CustomerQueryService(context);
            var result = await handler.GetAsync(1);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public async Task TryGetExistUser()
        {
            var context = ConfigurationContext.GetContext();
            var name = "Juan";

            await context.Clients.AddAsync(new()
            {
                Name = name
            });

            await context.SaveChangesAsync();

            var handler = new CustomerQueryService(context);
            var result = await handler.GetAsync(1);

            Assert.AreEqual(name, result.Name);
        }
    }
}