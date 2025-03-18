using AzureFunctions.Interfaces;
using AzureFunctions.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Infrastructure.Context;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddSingleton<IServiceBusSenderService, ServiceBusSenderService>();
builder.Services.AddSingleton<SaveToSqlService>();

builder.ConfigureFunctionsWebApplication();

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddServiceBusClient(Environment.GetEnvironmentVariable("ServiceBusConnectionString"));
});

string connectionString = Environment.GetEnvironmentVariable("AzureDbConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Build().Run();