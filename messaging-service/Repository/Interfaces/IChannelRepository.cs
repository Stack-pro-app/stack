using messaging_service.models.domain;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Requests;

namespace messaging_service.Repository.Interfaces
{
    public interface IChannelRepository
    {
        Task<bool> CreateChannelAsync(Channel channel);
        Task<bool> DeleteChannelAsync(int channelId);
        Task<bool> UpdateChannelAsync(Channel channel);
        Task<ChannelDetailDto> GetChannelAsync(int channelId);
        Task<IEnumerable<ChannelMinimalDto>> GetChannelsByWorkspaceAsync(int workspaceId);
        Task<bool> AddUserToPrivateChannel(int channelId,int userId);
        Task<bool> RemoveUserFromPrivateChannel(int channelId, int userId);
        Task<Channel> CreateOneToOneChannel(OneToOneChannelRequest request);
        Task<Channel?> GetOneToOneChannel(OneToOneChannelRequest request);
    }
}
