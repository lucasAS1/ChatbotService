using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotService.Models.Interfaces.Facades;
using RabbitMQ.Client.Core.DependencyInjection.MessageHandlers;
using RabbitMQ.Client.Core.DependencyInjection.Models;
using static System.Text.Encoding;
using static Newtonsoft.Json.JsonConvert;

namespace ChatbotService.Application.WebApi.MessageHandlers;

public class ChatbotMessageHandler : IAsyncMessageHandler
{
    private readonly IChatbotMessagingFacade _chatbotMessagingFacade;

    public ChatbotMessageHandler(IChatbotMessagingFacade chatbotMessagingFacade)
    {
        _chatbotMessagingFacade = chatbotMessagingFacade;
    }

    public async Task Handle(MessageHandlingContext context, string matchingRoute)
    {
        var message = UTF8.GetString(context.Message.Body.ToArray());
        var messageRequest = DeserializeObject<MessageRequest>(message);

        await _chatbotMessagingFacade.SendMessageAsync(messageRequest);
    }
}
