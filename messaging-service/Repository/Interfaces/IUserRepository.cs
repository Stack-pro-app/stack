using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int authId);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserAsync(int authId);
        Task<IEnumerable<User>> GetUsersByChannelAsync(int channelId);
        Task<IEnumerable<User>> GetUsersByWorkspaceAsync(int workspaceId);
    }
}
