using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;
using ChatbotService.Infrastructure.Interfaces.Agents;
using From = ChatbotService.Domain.Models.Responses.From;

namespace ChatbotService.Domain.UnitTests.stubs;

[ExcludeFromCodeCoverage]
public class StubAgent : IBotFrameworkAgent
{
    private readonly IFixture _fixture;

    public StubAgent(IFixture fixture)
    {
        _fixture = fixture;
    }

    public Task<List<Activity>> SendMessageAsync(ChatbotMessageRequest message)
    {
        message.Conversation ??= _fixture.Create<BfConversation>();
        
        var activities = _fixture.CreateMany<Activity>().ToList();
        activities
            .Add(_fixture.Build<Activity>()
                .With(x => x.From, new From() { Name = "ChatbotPessoal" })
                .With(x => x.SuggestedActions, new SuggestedActions(){Actions = _fixture.CreateMany<Action>(5).ToList()})
                .Create());

        return Task.FromResult(activities);
    }
}