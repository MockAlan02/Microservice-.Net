using Api.Gateway.Domain.Order.Command;
using Api.Gateway.Domain.Order.DTOs;

namespace Api.Gateway.Proxies.Interfaces
{
    public interface IOrderProxy : IGenericProxy<OrderCreateCommand, OrderDto, int>
    {
    }
}
