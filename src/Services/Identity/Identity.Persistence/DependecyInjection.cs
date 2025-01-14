using Identity.Domain;
using Identity.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Persistence
{
    public static class DependecyInjection
    {

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            #region identityContext
            services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsHistoryTable("_EFMigrationsHistory", "Identity"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            #endregion
        }
    }
}
