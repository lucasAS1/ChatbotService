using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class GetActivitiesResponse
{
    [JsonProperty("activities")]
    public List<Activity> Activities { get; set; } = null!;

    [JsonProperty("watermark")]
    public long Watermark { get; set; }
}