using Order.Persistence;
using Order.Service.Proxies;
using System.Reflection;
using Order.Api;
using Order.Services.Queries;
using Order.Service.Proxies.Catalog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ApiUrls
builder.Services.Configure<ApiUrls>(opt =>
{
   builder.Configuration.GetSection("ApiUrls").Bind(opt);
});

builder.Services.AddAuthorization();


//Add Automapper 
builder.Services.AddAutoMapper();

//JWt configuration
builder.Services.AddAuthenticationConfigure(builder.Configuration);

//AzureServicesBus
builder.Services.Configure<AzureServiceBus>(opt =>
{
    builder.Configuration.GetSection("AzureServiceBus").Bind(opt);
});

//Add Services
builder.Services.AddServices();

//Add Proxies Configure
builder.Services.AddConfigureProxies();

builder.Services.AddHttpContextAccessor();
//Add DbContext
builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.Load("Order.Services.EventHandler"));
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
