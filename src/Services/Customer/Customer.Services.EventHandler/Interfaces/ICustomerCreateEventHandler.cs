using Customer.Services.EventHandler.Command;

namespace Customer.Services.EventHandler.Interfaces
{
    public interface ICustomerCreateEventHandler
    {
        Task Handle(CustomerCreateCommand notification, CancellationToken cancellationToken);
    }
}