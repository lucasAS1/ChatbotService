using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ChatbotService.Domain.Models.Responses;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Requests;

[ExcludeFromCodeCoverage]
public class ChatbotMessageRequest
{
    [JsonProperty("locale")]
    [JsonPropertyName("locale")]
    public string Locale { get; set; } = "pt-BR";

    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = "message";

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
    public BfConversation? Conversation { get; set; }
    
    [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("channel")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [Newtonsoft.Json.JsonIgnore]
    public string? Channel { get; set; }

    [JsonProperty("watermark", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("watermark")]
    [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [Newtonsoft.Json.JsonIgnore]
    public string Watermark { get; set; } = "0";
}