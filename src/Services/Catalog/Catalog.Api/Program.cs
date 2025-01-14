using Catalog.Persistence;
using Catalog.Persistence.Seeder;
using Catalog.Services.Queries;
using Catalog.Services.Queries.Interfaces;
using Catalog.Api;
using Serilog;
using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductQueryService, ProductQueryService>();

//Configure DbContext
builder.Services.ConfigureDbContext(builder.Configuration);

//Configure Serialog
builder.Host.UseCustomSerilog();

builder.Services.AddSerilog();

//Configure Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthenticationConfigure(builder.Configuration);

//Configuring health check
builder.Services.ConfigureHealthChecks(builder.Configuration);

builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssemblies(Assembly.Load("Catalog.Service.EventHandlers")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scoped = app.Services.CreateScope())
{
    try
    {
        ApplicationDbContext context = scoped.ServiceProvider.GetService<ApplicationDbContext>()!;
        if (context is null)
        {
            Log.Logger.Error("Can't obtain context");
            return;
        }

        if (!await context.Database.CanConnectAsync())
        {
            Log.Logger.Error("Can't connect with the database. Seed doesn't executed");
            return;
        }

        await ProductSeeder.SeedAsync(context);
        await ProductInStockSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        Log.Logger.Error($"Can't connect with the database: {ex.Message}");
    }
}

//app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseAuthentication();
app.MapControllers();
app.MapHealthChecks("/api/healthz", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/healthcheck-ui";
});
app.Run();
