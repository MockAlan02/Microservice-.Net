using Customer.Domain;
using Customer.Persistence.Context;
using Customer.Services.EventHandler.Command;
using Customer.Services.EventHandler.Exceptions;
using Customer.Services.EventHandler.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.Common.Mapping;

namespace Customer.Services.EventHandler
{
    public class CustomerCreateEventHandler(ApplicationDbContext context, ILogger<CustomerCreateEventHandler> logger) : INotificationHandler<CustomerCreateCommand>, ICustomerCreateEventHandler
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<CustomerCreateEventHandler> _logger = logger;
        public async Task Handle(CustomerCreateCommand notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(notification.Name))
            {
                _logger.LogError("Failed to create customer: Client name is null or empty.");
                throw new CustomerCreateEventHandlerException("Client Name should not be null or empty");
            }

            _logger.LogInformation("Creating a new client with the name: {ClientName}", notification.Name);

            var clientEntity = notification.MapTo<Client>();
            _logger.LogDebug("Mapped notification to Client entity: {@ClientEntity}", clientEntity);

            await _context.Clients.AddAsync(clientEntity!, cancellationToken);
            _logger.LogInformation("Client entity added to the database context: {ClientId}", clientEntity!.Id);

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Changes successfully saved to the database for client: {ClientId}", clientEntity.Id);

        }
    }
}
