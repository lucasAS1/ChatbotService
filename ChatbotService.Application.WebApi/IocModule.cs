using System.Diagnostics.CodeAnalysis;
using Autofac;
using ChatbotService.Domain.Services.Chatbot;
using ChatbotService.Infrastructure.Agents;
using ChatbotService.Infrastructure.Interfaces.Agents;
using ChatbotService.Models.Interfaces.Services;

namespace ChatbotService.Application.WebApi;

[ExcludeFromCodeCoverage]
public class IocContainer : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        ConfigureInfrastructureLayer(builder);
        ConfigureDomainLayer(builder);
    }

    private static void ConfigureInfrastructureLayer(ContainerBuilder builder)
    {
        builder.RegisterType<BotFrameworkAgent>().As<IBotFrameworkAgent>();
    }
    
    private static void ConfigureDomainLayer(ContainerBuilder builder)
    {
        builder.RegisterType<ChatbotMessagingService>().As<IChatbotMessagingService>();
    }
}