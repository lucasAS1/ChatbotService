﻿using System.IdentityModel.Tokens.Jwt;
using ChatbotProject.Common.Domain.Models.Requests;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;
using ChatbotService.Domain.Models.Requests;
using ChatbotService.Domain.Models.Responses;
using ChatbotService.Infrastructure.DTOS.Conversation;
using ChatbotService.Infrastructure.Interfaces.Agents;
using ChatbotService.Models.Interfaces.Services;

namespace ChatbotService.Domain.Services.Chatbot;

public class ChatbotMessagingService : IChatbotMessagingService
{
    private readonly IBotFrameworkAgent _agent;
    private readonly IRepository<Conversation> _conversationRepository;
    private readonly IRepository<Message> _messageRepository;

    public ChatbotMessagingService(
        IBotFrameworkAgent agent,
        IRepository<Conversation> conversationRepository,
        IRepository<Message> messageRepository)
    {
        _agent = agent;
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
    }

    public async Task<List<MessageRequest>> SendMessageAsync(ChatbotMessageRequest message)
    {
        await StartConversationIfNotStarted(message);

        var activities = await _agent.SendMessageAsync(message);
        var brokerMessages = new List<MessageRequest>();
        
        await UpsertConversationDocument(message);
        await SaveMessageDocument(message, activities);

        CreateBrokerMessageList(activities, brokerMessages, message);

        return brokerMessages;
    }

    private async Task StartConversationIfNotStarted(ChatbotMessageRequest message)
    {
        var conversation = await _conversationRepository.GetDocument(x => x.UserId == message.From.Id);

        if (conversation is not null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadToken(conversation.Token) as JwtSecurityToken;
            
            if (token!.ValidTo < DateTime.Now)
            {
                return;
            }
            
            message.Watermark = conversation.NumberOfMessages;
            message.Conversation = new BfConversation()
            {
                Id = conversation.Id.ToString()!,
                Token = conversation.Token
            };
        }
    }

    private async Task UpsertConversationDocument(ChatbotMessageRequest message)
    {
        var conversationDocument = new Conversation()
        {
            Id = message.Conversation!.Id,
            UserId = message.From.Id,
            NumberOfMessages = message.Watermark,
            ActiveConversation = true,
            Token = message.Conversation!.Token
        };
        
        await _conversationRepository.AddOrUpdateDocument(conversationDocument);
    }

    private async Task SaveMessageDocument(ChatbotMessageRequest message, List<Activity> activities)
    {
        var messageDocument = new Message()
        {
            ConversationId = message.Conversation!.Id,
            Channel = BotChannel.Telegram,
            MessageReceived = message.Text,
            UserId = message.From.Id,
            Messages = activities.Select(x => x.Text).ToList()
        };

        await _messageRepository.AddOrUpdateDocument(messageDocument);
    }

    private void CreateBrokerMessageList(List<Activity> activities, List<MessageRequest> messageRequests, ChatbotMessageRequest message)
    {
        for (var i = 0; i < activities.Count; i++)
        {
            var activity = activities[i];
            if (activity.From.Name == "ChatbotPessoal")
            {
                var messageRequest = new MessageRequest() { Text = activity.Text, ChatId = message.From.Id };
                if(activity.SuggestedActions != null) AddInteractiveMessage(messageRequest, activity);

                messageRequests.Add(messageRequest);
            }
        }
    }

    private void AddInteractiveMessage(MessageRequest messageRequest, Activity activity)
    {
        messageRequest.InteractiveMessage = new InteractiveMessage
        {
            Type = activity.SuggestedActions.Actions.Count > 2 ? InteractiveMessageType.Menu : InteractiveMessageType.Button,
            Options = new List<string>()
        };

        foreach (var option in activity.SuggestedActions.Actions)
        {
            messageRequest.InteractiveMessage.Options.Add(option.Value);
        }
    }
}