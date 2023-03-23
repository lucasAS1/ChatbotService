using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Models.Interfaces.Facades;
using ChatbotService.Models.Interfaces.Services;
using RabbitMQ.Client.Core.DependencyInjection.Services.Interfaces;

namespace ChatbotService.Domain.Facades;

public class ChatbotMessagingFacade : IChatbotMessagingFacade
{
    private readonly IChatbotMessagingService _chatbotMessageService;
    private readonly IProducingService _producingService;

    public ChatbotMessagingFacade(IChatbotMessagingService chatbotMessageService, IProducingService producingService)
    {
        _chatbotMessageService = chatbotMessageService;
        _producingService = producingService;
    }

    public async Task SendMessageAsync(MessageRequest messageRequest)
    {
        var chatbotMessageRequest = new ChatbotMessageRequest()
            { From = new From() { Id = messageRequest!.ChatId }, Text = messageRequest.Text };

        var messages = await _chatbotMessageService.SendMessageAsync(chatbotMessageRequest);
        
        foreach (var message in messages)
        {
            _producingService.Send(message,"service-telegram","service-to-telegram");
        }
    }
}