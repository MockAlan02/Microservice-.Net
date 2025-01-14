using Api.Gateway.Domain.Customer.Command;
using Api.Gateway.Domain.Customer.DTOs;
using Api.Gateway.Proxies.Interfaces;
using Api.Gateway.Proxies.Urls;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Api.Gateway.Proxies.Proxies
{
    public class CustomerProxy(IOptions<ApiUrls> options, HttpClient client, IHttpContextAccessor httpContext) : GenericProxy<CustomerCreateCommand, ClientDto, int>(options, client, httpContext), ICustomerProxy
    {
        protected override string GetBaseUrl()
        {
            return _urls.CustomerUrl;
        }
    }
}
