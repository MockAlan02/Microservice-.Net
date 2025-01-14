using Api.Gateway.Domain.Catalog.Commands;
using Api.Gateway.Domain.Catalog.DTOs;

namespace Api.Gateway.WebClient.Proxy.Interfaces
{
    public interface ICatalogProxy : IGenericProxy<ProductCreateCommand, ProductDto, int>
    {
    }
}
