using messaging_service.models.domain;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Requests;

namespace messaging_service.Repository.Interfaces
{
    public interface IChannelRepository
    {
        Task CreateChannelAsync(Channel channel);
        Task DeleteChannelAsync(int channelId);
        Task UpdateChannelAsync(Channel channel);
        Task<ChannelDetailDto> GetChannelAsync(int channelId);
        Task<IEnumerable<ChannelMinimalDto>> GetChannelsByWorkspaceAsync(int workspaceId);
        Task AddUserToPrivateChannel(int channelId,int userId);
        Task RemoveUserFromPrivateChannel(int channelId, int userId);
        Task<Channel> CreateOneToOneChannel(OneToOneChannelRequest request);
        Task<Channel?> GetOneToOneChannel(OneToOneChannelRequest request);
        Task<List<string>> GetChannelNotificationStrings(int channelId);
        public Task<bool> VerifyAccess(string authId, int Channel_Id);
    }
}
