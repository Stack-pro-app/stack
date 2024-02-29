using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IChannelRepository
    {
        bool CreateChannel(Channel channel);
        bool DeleteChannel(int channelId);
        bool UpdateChannel(Channel channel);
        Channel GetChannel(int channelId);
        IEnumerable<Channel> GetChannelsByWorkspace(int workspaceId);
        IEnumerable<Channel> GetChannelsByBoth(int userId,int workspaceId);
    }
}
