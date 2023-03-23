using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotService.Domain.Models.Requests;

namespace ChatbotService.Models.Interfaces.Services;

public interface IChatbotMessagingService
{
    public Task<List<MessageRequest>> SendMessageAsync(ChatbotMessageRequest message);
}