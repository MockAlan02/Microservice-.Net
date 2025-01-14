using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.Domain.Settings;
using System.Text;

namespace Order.Api
{
    public static class DependecyInjection
    {
        public static void AddAuthenticationConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Token Configure
            ArgumentNullException.ThrowIfNull(configuration);
            var jwtSetting = new JwtSetting();
            configuration.GetSection("JwtSettings").Bind(jwtSetting);

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

            #region Swagger Configuration 
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
            #endregion
        }
    }
}
