using System.Text.Json.Serialization;
using ChatbotService.Domain.Models.Responses;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Requests;

public class ChatbotMessageRequest
{
    [JsonProperty("locale")]
    [JsonPropertyName("locale")]
    public string Locale { get; set; } = null!;

    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonProperty("from")]
    [JsonPropertyName("from")]
    public From From { get; set; } = null!;

    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; } = null!;
    
    [JsonProperty("conversation", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("conversation")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [Newtonsoft.Json.JsonIgnore]
    public Conversation? Conversation { get; set; }
}