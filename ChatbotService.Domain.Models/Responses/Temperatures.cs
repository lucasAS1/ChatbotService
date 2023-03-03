using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

public class Temperatures
{
    [JsonProperty("activities")]
    public List<Activity> Activities { get; set; } = null!;

    [JsonProperty("watermark")]
    public long Watermark { get; set; }
}