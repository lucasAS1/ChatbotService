namespace ChatbotService.Domain.Models.Broker;

public class InteractiveMessage
{
    public InteractiveMessageType Type { get; set; }
    public List<string> Options { get; set; } = null!;
}