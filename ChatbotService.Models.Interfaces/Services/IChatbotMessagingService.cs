using ChatbotService.Domain.Models.Broker;
using ChatbotService.Domain.Models.Requests;

namespace ChatbotService.Models.Interfaces.Services;

public interface IChatbotMessagingService
{
    public Task<List<BrokerMessage>> SendMessageAsync(ChatbotMessageRequest message);
}