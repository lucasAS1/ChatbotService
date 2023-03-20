using System.Diagnostics.CodeAnalysis;
using Autofac;
using ChatbotProject.Common.Infrastructure.Mongo;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using ChatbotService.Domain.Models.Settings;
using ChatbotService.Domain.Services.Chatbot;
using ChatbotService.Infrastructure.Agents;
using ChatbotService.Infrastructure.DTOS.Conversation;
using ChatbotService.Infrastructure.Interfaces.Agents;
using ChatbotService.Models.Interfaces.Services;
using Microsoft.Extensions.Options;

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
        builder.Register<Repository<Conversation>>(c =>
            {
                var config = c.Resolve<IOptions<Settings>>();
                return new Repository<Conversation>(config);
            })
            .As<IRepository<Conversation>>();
        builder.Register<Repository<Message>>(c =>
            {
                var config = c.Resolve<IOptions<Settings>>();
                return new Repository<Message>(config);
            })
            .As<IRepository<Message>>();
    }
}