using ChatbotProject.Common.Infrastructure.Mongo;

namespace ChatbotService.Infrastructure.DTOS.Conversation;

public class Conversation : BaseEntity
{
    public string UserId { get; set; } = null!;
    public string NumberOfMessages { get; set; } = null!;
    public bool ActiveConversation { get; set; }
    public int SurveyRating { get; set; }
    public string Token { get; set; } = null!;
}