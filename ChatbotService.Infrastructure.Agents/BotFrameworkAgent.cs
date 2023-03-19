using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;
using ChatbotService.Domain.Models.Settings;
using ChatbotService.Infrastructure.Interfaces.Agents;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;

namespace ChatbotService.Infrastructure.Agents;

public class BotFrameworkAgent : IBotFrameworkAgent
{
    private readonly string _token;

    public BotFrameworkAgent(IOptions<Settings> config)
    {
        _token = config.Value.BotFrameworkSettings.Token;
    }
    
    public async Task<List<Activity>> SendMessageAsync(ChatbotMessageRequest message)
    {
        if (message.Conversation == null)
        {
            var newConversation = await GenerateConversationToken();

            message.Conversation = new BfConversation()
            {
                Id = newConversation.ConversationId,
                Token = newConversation.Token
            };
        }
        else
        {
            var refreshedConversation = await RefreshConversationToken(message);

            message.Conversation = new BfConversation()
            {
                Id = refreshedConversation.ConversationId,
                Token = refreshedConversation.Token
            };
        }
        
        await SendActivity(message);

        return await GetActivities(message);
    }

    private async Task SendActivity(ChatbotMessageRequest message)
    {
        await Policy
            .Handle<FlurlHttpException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () =>
            {
                await
                    $"https://directline.botframework.com/v3/directline/conversations/{message.Conversation!.Id}/activities"
                        .WithHeader("Authorization", $"Bearer {message.Conversation.Token}")
                        .PostJsonAsync(message);
            });

        //todo: need to make a more intelligent way to garantee that the bot framework will have sent all messages
        await Task.Delay(2000);
    }

    private async Task<List<Activity>> GetActivities(ChatbotMessageRequest message)
    {
        var response = await Policy
            .Handle<FlurlHttpException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () =>
            {
                var activityListResponse = await
                    $"https://directline.botframework.com/v3/directline/conversations/{message.Conversation!.Id}/activities"
                        .SetQueryParam("watermark", message.Watermark)
                        .WithHeader("Authorization", $"Bearer {message.Conversation.Token}")
                        .GetJsonAsync<GetActivityResponse>();

                return activityListResponse;
            });
        var watermark = int.Parse(response.Watermark) + response.Activities.Count - 1;
        message.Watermark = watermark.ToString();

        return response.Activities;
    }
    
    private async Task<ConversationResponse> GenerateConversationToken()
    {
        var response = await Policy
            .Handle<FlurlHttpException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () =>
            {
                var conversationResponse = await "https://directline.botframework.com/v3/directline/conversations"
                    .WithHeader("Authorization", $"Bearer {_token}")
                    .PostAsync()
                    .ReceiveJson<ConversationResponse>();
                return conversationResponse;
            });

        return response;
    }

    private async Task<ConversationResponse> RefreshConversationToken(ChatbotMessageRequest message)
    {
        // Define the request URL
        var requestUrl = "https://directline.botframework.com/v3/directline/tokens/refresh";
        
        // Send the HTTP request using Flurl and the retry policy
        var response = await Policy
            .Handle<FlurlHttpException>((ex) =>
                throw new Exception($"Failed to refresh Direct Line token: {ex.Message}"))
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => await requestUrl
                .WithHeader("Authorization", $"Bearer {message.Conversation!.Token}")
                .PostAsync()
                .ReceiveJson<ConversationResponse>());
        
        return response;
    }
}