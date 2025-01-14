using Api.Gateway.WebClient.Proxy.Interfaces;
using Api.Gateway.WebClient.Proxy.Proxies;
using Api.Gateway.WebClient.Proxy.Urls;
using Clients.WebClient.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Clients.WebClient
{
    public static class DepdendecyInjection
    {
        public static void AddConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            #region Token Configure
            services.AddAuthentication();
            JwtSetting jwtSetting = new();
            configuration.GetSection("jwtSetting").Bind(jwtSetting);

            ArgumentException.ThrowIfNullOrWhiteSpace(jwtSetting.Key);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = jwtSetting.Issuer,
                 ValidAudience = jwtSetting.Audience,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key))
             };

             options.Events = new JwtBearerEvents
             {
                 OnAuthenticationFailed = context =>
                 {
                     context.NoResult();
                     context.Response.StatusCode = 401;
                     context.Response.ContentType = "text/plain";
                     return context.Response.WriteAsync("Invalid Token.");
                 }
             };
         });
            #endregion
        }
        
        public static void AddProxiesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiUrls>(configuration.GetSection("ApirUrls"));

            #region Proxies
            services.AddHttpClient<ICatalogProxy, CatalogProxy>();
            services.AddHttpClient<ICustomerProxy, CustomerProxy>();
            services.AddHttpClient<IOrderProxy, OrderProxy>();
            #endregion
        }
    }
}
