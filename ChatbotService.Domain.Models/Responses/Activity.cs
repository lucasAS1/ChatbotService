using System.Text.Json.Serialization;

namespace ChatbotService.Domain.Models.Responses;

public class Activity
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }

    [JsonPropertyName("from")]
    public From From { get; set; }

    [JsonPropertyName("conversation")]
    public Conversation Conversation { get; set; }

    [JsonPropertyName("textFormat")]
    public string TextFormat { get; set; }

    [JsonPropertyName("locale")]
    public string Locale { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("inputHint")]
    public string InputHint { get; set; }

    [JsonPropertyName("suggestedActions")]
    public SuggestedActions SuggestedActions { get; set; }

    [JsonPropertyName("attachments")]
    public List<dynamic> Attachments { get; set; }

    [JsonPropertyName("entities")]
    public List<dynamic> Entities { get; set; }

    [JsonPropertyName("replyToId")]
    public string ReplyToId { get; set; }
}