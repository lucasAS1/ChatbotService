using System.Diagnostics.CodeAnalysis;

namespace ChatbotService.Domain.Models.Settings;

[ExcludeFromCodeCoverage]
public class BotFrameworkSettings
{
    public string Token { get; set; } = null!;
}