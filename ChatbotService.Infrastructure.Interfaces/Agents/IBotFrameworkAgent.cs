using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;

namespace ChatbotService.Infrastructure.Interfaces.Agents;

public interface IBotFrameworkAgent
{
    Task<List<Activity>> SendMessageAsync(ChatbotMessageRequest message);
}