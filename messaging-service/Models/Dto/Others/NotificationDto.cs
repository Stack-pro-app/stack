﻿namespace messaging_service.Models.Dto.Others
{
    public class NotificationDto
    {
        public string Message { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string MailTo { get; set; } = null!;
    }
}
