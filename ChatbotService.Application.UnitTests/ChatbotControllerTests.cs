using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotService.Application.WebApi.Controllers;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Models.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ChatbotService.Application.UnitTests;

public class ChatbotControllerTests
{
    private readonly Mock<IChatbotMessagingService> _mockService;
    private readonly IFixture _fixture;

    public ChatbotControllerTests()
    {
        _mockService = new Mock<IChatbotMessagingService>();
        _fixture = new Fixture();

        _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
    }
    
    [Fact]
    public async Task SendMessageCorrectly()
    {
        var uat = new ChatbotController(_mockService.Object);
        var messageMock = _fixture.Create<ChatbotMessageRequest>();
        _mockService.Setup(x => x.SendMessageAsync(It.IsAny<ChatbotMessageRequest>()))
            .ReturnsAsync(_fixture.CreateMany<MessageRequest>().ToList());
        
        var result = await uat.SendMessage(messageMock);
        
        Assert.IsType<OkObjectResult>(result);
    }
}