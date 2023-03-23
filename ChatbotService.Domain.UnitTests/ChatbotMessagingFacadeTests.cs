using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotService.Domain.Facades;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Models.Interfaces.Services;
using Moq;
using RabbitMQ.Client.Core.DependencyInjection.Services.Interfaces;
using Xunit;

namespace ChatbotService.Domain.UnitTests;

public class ChatbotMessagingFacadeTests
{
    private readonly Mock<IChatbotMessagingService> _chatbotMessageServiceMock;
    private readonly Mock<IProducingService> _producingServiceMock;
    private readonly IFixture _fixture;

    public ChatbotMessagingFacadeTests()
    {
        _chatbotMessageServiceMock = new Mock<IChatbotMessagingService>();
        _producingServiceMock = new Mock<IProducingService>();
        _fixture = new Fixture();

        ConfigureMocks();
    }

    private void ConfigureMocks()
    {
        _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

        _chatbotMessageServiceMock.Setup(x => x.SendMessageAsync(It.IsAny<ChatbotMessageRequest>()))
            .ReturnsAsync(_fixture.CreateMany<MessageRequest>().ToList());
        _producingServiceMock.Setup(x => x.Send(
                It.IsAny<MessageRequest>(), It.IsAny<string>(), It.IsAny<string>()))
            .Verifiable();
    }

    [Fact]
    public async Task ShouldSendMessageCorrectly()
    {
        var uat = new ChatbotMessagingFacade(
            _chatbotMessageServiceMock.Object,
            _producingServiceMock.Object);

        var messageRequest = _fixture.Create<MessageRequest>();
        await uat.SendMessageAsync(messageRequest);
        
        _producingServiceMock.Verify(x => x.Send(It.IsAny<MessageRequest>(), It.IsAny<string>(), It.IsAny<string>()),Times.AtLeastOnce);
        _producingServiceMock.Verify(x => x.Send(messageRequest, It.IsAny<string>(), It.IsAny<string>()),Times.Never);
        _chatbotMessageServiceMock.Verify(x => x.SendMessageAsync(It.IsAny<ChatbotMessageRequest>()), Times.Once);
    }
}