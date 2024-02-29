﻿using messaging_service.models.domain;
namespace messaging_service.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<bool> CreateChatAsync(Chat message);
        Task<bool> DeleteChatPartAsync(int messageId);
        Task<bool> DeleteChatPermAsync(int messageId);
        Task<bool> UpdateChatAsync(int messageId, string message);
        Task<Chat> GetMessageAsync(int messageId);
        Task<IEnumerable<Chat>> GetChannelMessageAsync(int channelId);
    }
}
