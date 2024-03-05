using Microsoft.AspNetCore.SignalR;
using gateway_chat_server.Models;
using Newtonsoft.Json;
using gateway_chat_server.Producer;
namespace gateway_chat_server.Hubs
{
    public class ChannelHub : Hub
    {
        private readonly IMessageProducer _messagePublisher;

        public ChannelHub(IMessageProducer messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }
        // We Can also track if a user is online using these methods
        // Step 1: Add the client to the channel (everytime)
        public async Task AddToGroup(string channel)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channel);

            await Clients.Group(channel).SendAsync("Send", $"{Context.ConnectionId} has joined the group {channel}.");
        }

        //Step 2: Send Messages Using this Method
        public async Task SendMessage(string root)
        {
            Chat message= JsonConvert.DeserializeObject<Chat>(root);
            ChatDto messageToStore = new()
            {
                UserId = message.UserId,
                ChannelId = message.ChannelId,
                Message = message.Message,
                ParentId = message.ParentId,
            };
            _messagePublisher.SendMessage(message);
            await Clients.Group(message.ChannelString).SendAsync("messageReceived", root);
        }
        //Step 3: Remove the client From the channel to keep it clean
        public async Task RemoveFromGroup(string channel)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);

            await Clients.Group(channel).SendAsync("Send", $"{Context.ConnectionId} has left the group {channel}.");
        }

    }
}
