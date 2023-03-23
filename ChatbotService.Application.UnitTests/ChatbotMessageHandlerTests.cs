using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotService.Application.WebApi.MessageHandlers;
using ChatbotService.Models.Interfaces.Facades;
using Moq;
using RabbitMQ.Client.Core.DependencyInjection.Models;
using RabbitMQ.Client.Events;
using Xunit;

namespace ChatbotService.Application.UnitTests;

public class ChatbotMessageHandlerTests
{
    private readonly Mock<IChatbotMessagingFacade> _chatbotMessagingFacadeMock;
    private readonly IFixture _fixture;

    public ChatbotMessageHandlerTests()
    {
        _fixture = new Fixture();
        _chatbotMessagingFacadeMock = new Mock<IChatbotMessagingFacade>();
       
        _fixture.Customize(new AutoMoqCustomization(){ConfigureMembers = true});
    }
    
    private void ConfigureMocks()
    {
        _chatbotMessagingFacadeMock
            .Setup(x => x.SendMessageAsync(It.IsAny<MessageRequest>()))
            .Returns(Task.CompletedTask);
    }
    
    
    [Fact]
    public async Task ShouldProperlyHandleMessageReceivingEvent()
    {
        ConfigureMocks();
        var argsMock = _fixture
            .Build<BasicDeliverEventArgs>()
            .With(x => x.Body, Encoding.ASCII.GetBytes("{\"text\":\"teste\",\"chatId\":\"1234364\"}"))
            .WithAutoProperties()
            .Create();
        
        var aut = new ChatbotMessageHandler(_chatbotMessagingFacadeMock.Object);
        var messageHandlingContext = new MessageHandlingContext(argsMock, x => x.Exchange = "testexchange", false);
        
        await aut.Handle(messageHandlingContext, "test-route");
        Assert.Equal(messageHandlingContext.Message.Exchange, argsMock.Exchange);
    }
}