using Api.Gateway.WebClient.Proxy.Interfaces;
using Api.Gateway.WebClient.Proxy.Proxies;
using Api.Gateway.WebClient.Proxy.Urls;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.WebClient.Proxy
{
    public static class DependecyInjection
    {
        public static void AddServicesProxies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configure Options
            services.Configure<ApiUrls>(configuration.GetSection("ApiUrls"));
            #endregion


            #region Add Services
            services.AddHttpClient<ICatalogProxy, CatalogProxy>();
            services.AddHttpClient<IOrderProxy, OrderProxy>();
            services.AddHttpClient<ICustomerProxy, CustomerProxy>();
            #endregion
        }
    }
}
