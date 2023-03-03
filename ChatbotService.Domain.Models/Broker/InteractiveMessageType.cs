using Newtonsoft.Json;

namespace ChatbotService.Domain.Models.Broker;

public enum InteractiveMessageType
{
    [JsonProperty("Button")]
    Button,
    [JsonProperty("Menu")]
    Menu
}