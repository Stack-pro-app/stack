using Microsoft.AspNetCore.SignalR;
using gateway_chat_server.Models;
using Newtonsoft.Json;
namespace gateway_chat_server.Hubs
{
    public class ChannelHub : Hub
    {

        public async Task SendMessage(string root)
        {
            Chat message= JsonConvert.DeserializeObject<Chat>(root);
            await Clients.Group(message.ChannelId.ToString()).SendAsync("messageReceived", root);
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
