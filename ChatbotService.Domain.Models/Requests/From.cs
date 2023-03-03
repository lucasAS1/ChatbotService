using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Requests;

public class From
{
    [JsonProperty("id")]
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;
}