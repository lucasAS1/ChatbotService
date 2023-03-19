using ChatbotProject.Common.Infrastructure.Mongo;

namespace ChatbotService.Infrastructure.DTOS.Conversation;

public class Message : BaseEntity
{
    public string ConversationId { get; set; } = null!;
    public List<string> Messages { get; set; } = null!;
    public string MessageReceived { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public BotChannel? Channel { get; set; }
}