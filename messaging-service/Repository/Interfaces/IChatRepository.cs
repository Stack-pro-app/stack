using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;

namespace messaging_service.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task CreateChatAsync(Chat message);
        Task DeleteChatPartAsync(int messageId);
        Task DeleteChatPermAsync(int messageId);
        Task DeleteChatPermAsync(Guid messageId);
        Task UpdateChatAsync(Guid messageId, string message);
        Task<MessageDetailDto> GetMessageAsync(int messageId);
        Task<IEnumerable<MessageDetailDto>> GetChannelLastMessagesAsync(int channelId,int page);
    }
}
