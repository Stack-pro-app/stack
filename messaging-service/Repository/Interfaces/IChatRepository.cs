using messaging_service.models.domain;
namespace messaging_service.Repository.Interfaces
{
    public interface IChatRepository
    {
        bool CreateChat(Chat message);
        bool DeleteChatPart(int messageId);
        bool DeleteChatPerm(int messageId);
        bool UpdateChat(int messageId,string message);
        Chat GetMessage(int messageId);
        IEnumerable<Chat> GetChannelMessage(int channelId);
    }
}
