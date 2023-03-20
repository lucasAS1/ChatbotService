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
        
        /// <summary>
        /// Send a message and receive the responses from the chatbot.
        /// This is a temporary endpoint and will be removed from the
        /// official chatbot api when it's first working release gets launched.
        ///</summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatbotMessageRequest message)
        {
            var result = await _chatbotMessagingService.SendMessageAsync(message);

            return Ok(result);
        }
    }
}
