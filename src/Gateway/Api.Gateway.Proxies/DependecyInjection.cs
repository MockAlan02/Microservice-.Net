using Api.Gateway.Proxies.Interfaces;
using Api.Gateway.Proxies.Proxies;
using Api.Gateway.Proxies.Urls;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Gateway.Proxies
{
    public static class DependecyInjection
    {
        public static void AddServicesProxies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configure Options
            services.Configure<ApiUrls>(opt =>
            {
                configuration.GetSection("ApiUrls").Bind(opt);
            });
            #endregion


            #region Add Services
            services.AddHttpClient<ICatalogProxy, CatalogProxy>();
            services.AddHttpClient<IIdentityProxy, IdentityProxy>();
            services.AddHttpClient<IOrderProxy, OrderProxy>();
            services.AddHttpClient<ICustomerProxy, CustomerProxy>();
            #endregion
        }
    }
}
