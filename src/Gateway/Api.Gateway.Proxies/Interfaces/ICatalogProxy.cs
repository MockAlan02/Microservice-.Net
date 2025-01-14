using Api.Gateway.Domain.Catalog.Commands;
using Api.Gateway.Domain.Catalog.DTOs;

namespace Api.Gateway.Proxies.Interfaces
{
    public interface ICatalogProxy : IGenericProxy<ProductCreateCommand, ProductDto, int>
    {
    }
}
