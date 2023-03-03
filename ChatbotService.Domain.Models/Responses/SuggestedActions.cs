using System.Text.Json.Serialization;

namespace ChatbotService.Domain.Models.Responses;

public class SuggestedActions
{
    [JsonPropertyName("actions")]
    public List<Action> Actions { get; set; }
}