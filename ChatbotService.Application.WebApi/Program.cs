using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChatbotService.Application.WebApi;
using ChatbotService.Application.WebApi.MessageHandlers;
using ChatbotService.Domain.Models.Settings;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using RabbitMQ.Client.Core.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddSwaggerGen();
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddAzureKeyVault(
        builder.Configuration["KeyVaultClientDNS"],
        builder.Configuration["KeyVaultClientId"],
        builder.Configuration["KeyVaultClientSecret"],
        new DefaultKeyVaultSecretManager())
    .AddEnvironmentVariables();
builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services
    .AddRabbitMqServices(builder.Configuration.GetSection("Settings:RabbitMqSettings:RabbitMqConnection"))
    .AddConsumptionExchange("telegram-service", builder.Configuration.GetSection("Settings:RabbitMqSettings:ExchangeService"))
    .AddProductionExchange("service-telegram", builder.Configuration.GetSection("Settings:RabbitMqSettings:ExchangeTelegram"))
    .AddAsyncMessageHandlerSingleton<ChatbotMessageHandler>("telegram-to-service");
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new IocContainer()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();