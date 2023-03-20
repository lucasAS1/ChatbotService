using System.Diagnostics.CodeAnalysis;

namespace ChatbotService.Domain.Models.Broker;

[ExcludeFromCodeCoverage]
public class InteractiveMessage
{
    public InteractiveMessageType Type { get; set; }
    public List<string> Options { get; set; } = null!;
}