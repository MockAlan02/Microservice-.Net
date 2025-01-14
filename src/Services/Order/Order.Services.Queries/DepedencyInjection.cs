using Microsoft.Extensions.DependencyInjection;
using Order.Services.Queries.Interfaces;
using Order.Services.Queries.Profile;

namespace Order.Services.Queries
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region AddQueryService
            services.AddTransient<IOrderQueryService, OrderQueryService>();
            #endregion

            return services;
        }


        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}
