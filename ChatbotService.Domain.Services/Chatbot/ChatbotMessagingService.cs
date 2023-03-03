using ChatbotService.Domain.Models.Broker;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;
using ChatbotService.Infrastructure.Interfaces.Agents;
using ChatbotService.Models.Interfaces.Services;

namespace ChatbotService.Domain.Services.Chatbot;

public class ChatbotMessagingService : IChatbotMessagingService
{
    private readonly IBotFrameworkAgent _agent;

    public ChatbotMessagingService(IBotFrameworkAgent agent)
    {
        _agent = agent;
    }

    public async Task<List<BrokerMessage>> SendMessageAsync(ChatbotMessageRequest message)
    {
        var activities = await _agent.SendMessageAsync(message);
        
        var brokerMessages = new List<BrokerMessage>();

        CreateBrokerMessageList(activities, brokerMessages, message);

        return brokerMessages;
    }

    private void CreateBrokerMessageList(List<Activity> activities, List<BrokerMessage> brokerMessages, ChatbotMessageRequest message)
    {
        for (var i = 0; i < activities.Count; i++)
        {
            var activity = activities[i];
            if (activity.From.Name == "ChatbotPessoal")
            {
                var brokerMessage = new BrokerMessage() { Text = activity.Text, ChatId = message.From.Id };
                if(activity.SuggestedActions != null) AddInteractiveMessage(brokerMessage, activity);

                brokerMessages.Add(brokerMessage);
            }
        }
    }

    private void AddInteractiveMessage(BrokerMessage brokerMessage, Activity activity)
    {
        brokerMessage.InteractiveMessage = new InteractiveMessage
        {
            Type = activity.SuggestedActions.Actions.Count > 2 ? InteractiveMessageType.Menu : InteractiveMessageType.Button,
            Options = new List<string>()
        };

        foreach (var option in activity.SuggestedActions.Actions)
        {
            brokerMessage.InteractiveMessage.Options.Add(option.Value);
        }
    }
}