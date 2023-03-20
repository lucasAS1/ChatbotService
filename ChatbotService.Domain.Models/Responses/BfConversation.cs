using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class BfConversation
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;
}