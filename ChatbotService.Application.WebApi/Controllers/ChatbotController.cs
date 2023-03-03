using ChatbotService.Domain.Models.Requests;
using ChatbotService.Models.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotService.Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotMessagingService _chatbotMessagingService;

        public ChatbotController(IChatbotMessagingService chatbotMessagingService)
        {
            _chatbotMessagingService = chatbotMessagingService;
        }
        
        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatbotMessageRequest message)
        {
            var result = await _chatbotMessagingService.SendMessageAsync(message);

            return Ok(result);
        }
    }
}
