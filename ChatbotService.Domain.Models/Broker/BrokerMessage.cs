using System.Diagnostics.CodeAnalysis;

namespace ChatbotService.Domain.Models.Broker;

[ExcludeFromCodeCoverage]
public class BrokerMessage
{
    public string ChatId { get; init; } = null!;
    public string Text { get; init; } = null!;
    public InteractiveMessage InteractiveMessage { get; set; } = null!;
}