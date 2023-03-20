using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class GetActivityResponse
{
    [JsonProperty("activities")]
    [JsonPropertyName("activities")]
    public List<Activity> Activities { get; set; } = null!;

    [JsonPropertyName("watermark")]
    [JsonProperty("watermark")]
    public string Watermark { get; set; } = null!;
}