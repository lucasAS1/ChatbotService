using ChatbotProject.Common.Domain.Models.Settings;

namespace ChatbotService.Domain.Models.Settings;

public class Settings : ApiSettings
{
    public BotFrameworkSettings BotFrameworkSettings { get; set; } = null!;
}