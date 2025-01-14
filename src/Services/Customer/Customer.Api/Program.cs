using Customer.Api;
using Customer.Persistence.Context;
using Customer.Persistence.Seeder;
using Customer.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Host.ConfigureSeriaLog();

builder.Services.AddAuthorization();

//Add Authentication
builder.Services.AddAuthenticationConfigure(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.Load("Customer.Services.EventHandler"));
});

//Configure Services
builder.Services.ConfigureCustomerServices();

//Configure DbContext
builder.Services.ConfigureContext(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scopped = app.Services.CreateScope();
var context = scopped.ServiceProvider.GetService<ApplicationDbContext>();
if (context!.Database.CanConnect())
{
    await ClientSeeder.Seeder(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
