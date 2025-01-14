using Customer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Persistence
{
    public static class DependecyInjection
    {
        public static void ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            #region AddMainContext
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsHistoryTable("_EFMigrationsHistory", "Customer"));
            });
            #endregion
        }
    }
}
