using Microsoft.AspNetCore.SignalR;
using gateway_chat_server.Models;
using Newtonsoft.Json;
namespace gateway_chat_server.Hubs
{
    public class ChannelHub : Hub

    {
        private readonly ILogger<ChannelHub> _logger;

        public ChannelHub(ILogger<ChannelHub> logger)
        {
            _logger = logger;
        }
        public async Task SendMessage(string root)
        {
            _logger.LogInformation("Messaging method worked ");
            Chat? message= JsonConvert.DeserializeObject<Chat>(root);
            _logger.LogInformation(message.Message);
            _logger.LogInformation(root);

            await Clients.Group(message.ChannelString.ToString()).SendAsync("messageReceived", root);
            
        }
        public async Task AddToGroup(string channel)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channel);

            await Clients.Group(channel).SendAsync("Send", $"{Context.ConnectionId} has joined the group {channel}.");
        }

        public async Task RemoveFromGroup(string channel)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);

            await Clients.Group(channel).SendAsync("Send", $"{Context.ConnectionId} has left the group {channel}.");
        }

    }
}
