using ChatbotService.Domain.Models.Broker;

namespace ChatbotService.Models.Interfaces.Facades;

public interface IChatbotMessagingFacade
{
    public Task SendMessageAsync(BrokerMessage brokerMessage);
}