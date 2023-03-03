using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

public class ConversationResponse
{
    [JsonProperty("conversationId")]
    public string ConversationId { get; set; } = null!;

    [JsonProperty("token")]
    public string Token { get; set; } = null!;

    [JsonProperty("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonProperty("streamUrl")]
    public string StreamUrl { get; set; } = null!;

    [JsonProperty("referenceGrammarId")]
    public string ReferenceGrammarId { get; set; } = null!;

    [JsonProperty("eTag")]
    public string ETag { get; set; } = null!;
}