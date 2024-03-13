﻿namespace gateway_chat_server.Models
{
    public class ChatDto
    {
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string? Message { get; set; }
        public int? ParentId { get; set; }
        public string? Attachement_Url { get; set; }
        public string? Attachement_Name { get; set; }
    }
}
