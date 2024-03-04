using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;

namespace messaging_service.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<bool> CreateChatAsync(Chat message);
        Task<bool> DeleteChatPartAsync(int messageId);
        Task<bool> DeleteChatPermAsync(int messageId);
        Task<bool> UpdateChatAsync(int messageId, string message);
        Task<MessageDetailDto> GetMessageAsync(int messageId);
        Task<IEnumerable<MessageDetailDto>> GetChannelLastMessagesAsync(int channelId,int page);
    }
}
