using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ChatbotService.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class Activity
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; } = null!;

    [JsonPropertyName("from")]
    public From From { get; set; } = null!;

    [JsonPropertyName("conversation")]
    public BfConversation BfConversation { get; set; } = null!;

    [JsonPropertyName("textFormat")]
    public string TextFormat { get; set; } = null!;

    [JsonPropertyName("locale")]
    public string Locale { get; set; } = null!;

    [JsonPropertyName("text")]
    public string Text { get; set; } = null!;

    [JsonPropertyName("inputHint")]
    public string InputHint { get; set; } = null!;

    [JsonPropertyName("suggestedActions")]
    public SuggestedActions SuggestedActions { get; set; } = null!;

    [JsonPropertyName("attachments")]
    public List<dynamic> Attachments { get; set; } = null!;

    [JsonPropertyName("entities")]
    public List<dynamic> Entities { get; set; } = null!;

    [JsonPropertyName("replyToId")]
    public string ReplyToId { get; set; } = null!;
}