using System.Text.Json.Serialization;

namespace ChatbotService.Domain.Models.Responses;

public class Action
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = null!;
}