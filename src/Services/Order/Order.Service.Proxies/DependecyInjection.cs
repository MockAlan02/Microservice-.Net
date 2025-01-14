using Microsoft.Extensions.DependencyInjection;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Interface;

namespace Order.Service.Proxies
{
    public static class DependecyInjection
    {
        public static void AddConfigureProxies(this IServiceCollection services)
        {
            services.AddHttpClient<ICatalogProxy, CatalogHttpProxy>();
        }
    }
}
