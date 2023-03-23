using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;
using ChatbotService.Domain.Services.Chatbot;
using ChatbotService.Domain.UnitTests.stubs;
using ChatbotService.Infrastructure.DTOS.Conversation;
using ChatbotService.Infrastructure.Interfaces.Agents;
using Moq;
using Xunit;
using From = ChatbotService.Domain.Models.Responses.From;

namespace ChatbotService.Domain.UnitTests;

public class ChatbotMessagingServiceTests
{
    private readonly Mock<IBotFrameworkAgent> _agentMock;
    private readonly IRepository<Conversation> _conversationRepositoryMock;
    private readonly IRepository<Message> _messageRepositoryMock;
    private readonly IFixture _fixture;

    public ChatbotMessagingServiceTests()
    {
        _agentMock = new Mock<IBotFrameworkAgent>();
        _fixture = new Fixture();

        _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        _conversationRepositoryMock = new StubRepository<Conversation>(_fixture);
        _messageRepositoryMock = new StubRepository<Message>(_fixture);
    }

    private void ConfigureMocks()
    {
        var activities = _fixture.CreateMany<Activity>().ToList();
        activities
            .Add(_fixture.Build<Activity>()
            .With(x => x.From, new From() { Name = "ChatbotPessoal" })
            .With(x => x.SuggestedActions, new SuggestedActions(){Actions = _fixture.CreateMany<Action>(5).ToList()})
            .Create());
        
        _agentMock.Setup(x => x.SendMessageAsync(It.IsAny<ChatbotMessageRequest>()))
            .ReturnsAsync(activities);
    }
    
    [Fact]
    public async Task ShouldSendMessageCorrectly()
    {
        var chatbotMessageRequest = _fixture
            .Build<ChatbotMessageRequest>()
            .With(x => x.Conversation, _fixture.Create<BfConversation>())
            .Create();
        ConfigureMocks();

        var uat = new ChatbotMessagingService(_agentMock.Object, _conversationRepositoryMock, _messageRepositoryMock);
        var result = await uat.SendMessageAsync(chatbotMessageRequest);

        Assert.IsType<List<MessageRequest>>(result);
    }
    
    [Fact]
    public async Task ShouldSendMessageCorrectlyWhenConversationDoesntExistAndStartIt()
    {
        var chatbotMessageRequest = _fixture
            .Build<ChatbotMessageRequest>()
            .With(x => x.Conversation, () => null)
            .Create();
        ConfigureMocks();

        var uat = new ChatbotMessagingService(_agentMock.Object, _conversationRepositoryMock, _messageRepositoryMock);
        var result = await uat.SendMessageAsync(chatbotMessageRequest);

        Assert.IsType<List<MessageRequest>>(result);
    }
}