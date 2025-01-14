using Clients.WebClient;
using Clients.WebClient.Domain.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

//Add Proxies
builder.Services.AddProxiesConfiguration(builder.Configuration);

// Add Token Configuration
//builder.Services.AddConfigureAuthentication(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddJwtBearer() // Configura JWT si también estás utilizando tokens
.AddCookie(opt =>
    {
        opt.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                // Redirige a la URL configurada en appsettings.json
                context.Response.Redirect(
                    builder.Configuration.GetValue<string>("LoginUrl")!
                );
                return Task.CompletedTask;
            }
        };

        opt.LoginPath = "/Account/Login"; // Ruta de login predeterminada
    });

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
