namespace ChatbotService.Domain.Models.Broker;

public class BrokerMessage
{
    public string ChatId { get; init; } = null!;
    public string Text { get; init; } = null!;
    public InteractiveMessage InteractiveMessage { get; set; }
}