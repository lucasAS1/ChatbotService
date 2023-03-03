using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

public class Conversation
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;
}