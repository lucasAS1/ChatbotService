using System.Diagnostics.CodeAnalysis;
using ChatbotProject.Common.Domain.Models.Settings;

namespace ChatbotService.Domain.Models.Settings;

[ExcludeFromCodeCoverage]
public class Settings : ApiSettings
{
    public BotFrameworkSettings BotFrameworkSettings { get; set; } = null!;
}