using Api.Gateway.Domain.Customer.Command;
using Api.Gateway.Domain.Customer.DTOs;

namespace Api.Gateway.Proxies.Interfaces
{
    public interface ICustomerProxy: IGenericProxy<CustomerCreateCommand, ClientDto, int>
    {
    }
}
