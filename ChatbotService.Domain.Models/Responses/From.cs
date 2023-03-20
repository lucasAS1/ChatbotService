using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class From
{
    [JsonProperty("id")]
    public string Id { get; set; } = null!;

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; } = null!;

    [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
    public string Role { get; set; } = null!;
}