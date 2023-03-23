using ChatbotProject.Common.Domain.Models.Requests;

namespace ChatbotService.Models.Interfaces.Facades;

public interface IChatbotMessagingFacade
{
    public Task SendMessageAsync(MessageRequest messageRequest);
}