using Api.Gateway.Domain.Customer.Command;
using Api.Gateway.Domain.Customer.DTOs;

namespace Api.Gateway.WebClient.Proxy.Interfaces
{
    public interface ICustomerProxy : IGenericProxy<CustomerCreateCommand, ClientDto, int>
    {
    }
}
