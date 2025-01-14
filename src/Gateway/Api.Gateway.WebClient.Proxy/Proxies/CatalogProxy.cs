using Api.Gateway.Domain.Catalog.Commands;
using Api.Gateway.Domain.Catalog.DTOs;
using Api.Gateway.WebClient.Proxy.Interfaces;
using Api.Gateway.WebClient.Proxy.Urls;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.Gateway.WebClient.Proxy.Proxies
{
    public class CatalogProxy(IOptions<ApiUrls> options, HttpClient client, IHttpContextAccessor httpContext) : GenericProxy<ProductCreateCommand, ProductDto, int>(options, client, httpContext), ICatalogProxy
    {
        protected override string GetBaseUrl()
        {
            return _urls.ApiGatewayUrl + "/api/Products";
        }
    }
}
