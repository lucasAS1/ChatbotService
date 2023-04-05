using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;
using ChatbotService.Domain.Services.Chatbot;
using ChatbotService.Domain.UnitTests.Mocks;
using ChatbotService.Domain.UnitTests.stubs;
using ChatbotService.Infrastructure.DTOS.Conversation;
using ChatbotService.Infrastructure.Interfaces.Agents;
using Microsoft.IdentityModel.Logging;
using Xunit;

namespace ChatbotService.Domain.UnitTests;

public class ChatbotMessagingServiceTests
{
    private readonly IBotFrameworkAgent _agentMock;
    private IRepository<Conversation> _conversationRepositoryMock = null!;
    private readonly IRepository<Message> _messageRepositoryMock;
    private readonly IFixture _fixture;

    public ChatbotMessagingServiceTests()
    {
        IdentityModelEventSource.ShowPII = true;
        _fixture = new Fixture();
        _agentMock = new StubAgent(_fixture);

        _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        _messageRepositoryMock = new StubRepository<Message>(_fixture, null);
    }

    [Fact]
    public async Task ShouldSendMessageCorrectly()
    {
        var bfConversation = _fixture.Create<BfConversation>();
        var claims = new List<Claim>()
        {
            new ("nome", "teste")
        };
        
        var token = JwtMock.GenerateJwtToken(claims, DateTime.Now.AddHours(2));

        var chatbotMessageRequest = _fixture
            .Build<ChatbotMessageRequest>()
            .With(x => x.Conversation, bfConversation)
            .Create();

        var conversation = _fixture.Build<Conversation>().With(x => x.Token, token).Create();
        _conversationRepositoryMock = new StubRepository<Conversation>(_fixture, conversation);

        var uat = new ChatbotMessagingService(_agentMock, _conversationRepositoryMock, _messageRepositoryMock);
        var result = await uat.SendMessageAsync(chatbotMessageRequest);

        Assert.IsType<List<MessageRequest>>(result);
    }

    [Fact]
    public async Task ShouldSendMessageCorrectlyWhenConversationHasExpiredAndStartNewConversation()
    {
        var bfConversation = _fixture.Create<BfConversation>();
        var claims = new List<Claim>()
        {
            new ("nome", "teste")
        };
        
        var token = JwtMock.GenerateJwtToken(claims, DateTime.Now.AddHours(-2));

        var chatbotMessageRequest = _fixture
            .Build<ChatbotMessageRequest>()
            .With(x => x.Conversation, bfConversation)
            .Create();

        var conversation = _fixture.Build<Conversation>().With(x => x.Token, token).Create();
        _conversationRepositoryMock = new StubRepository<Conversation>(_fixture, conversation);

        var uat = new ChatbotMessagingService(_agentMock, _conversationRepositoryMock, _messageRepositoryMock);
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

        _conversationRepositoryMock = new StubRepository<Conversation>(_fixture, null);

        var uat = new ChatbotMessagingService(_agentMock, _conversationRepositoryMock, _messageRepositoryMock);
        var result = await uat.SendMessageAsync(chatbotMessageRequest);

        Assert.IsType<List<MessageRequest>>(result);
    }
}