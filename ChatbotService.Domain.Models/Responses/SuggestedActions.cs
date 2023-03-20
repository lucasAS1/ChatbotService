using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ChatbotService.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public class SuggestedActions
{
    [JsonPropertyName("actions")] 
    public List<Action> Actions { get; set; } = null!;
}