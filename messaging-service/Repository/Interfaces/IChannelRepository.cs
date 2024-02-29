using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IChannelRepository
    {
        Task<bool> CreateChannelAsync(Channel channel);
        Task<bool> DeleteChannelAsync(int channelId);
        Task<bool> UpdateChannelAsync(Channel channel);
        Task<Channel> GetChannelAsync(int channelId);
        Task<IEnumerable<Channel>> GetChannelsByWorkspaceAsync(int workspaceId);
    }
}
