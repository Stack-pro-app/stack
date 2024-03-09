using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using System;

namespace gateway_chat_server.Models
{
    public class Chat
    {
        public string UserId { get; set; }
        public string ChannelId { get; set; }
        public string ChannelString { get; set; }
        public string Message { get; set; }
        public string? ParentId { get; set; }
    }
}
