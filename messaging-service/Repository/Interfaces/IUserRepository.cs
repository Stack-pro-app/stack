using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserAsync(int userId);
        Task<IEnumerable<User>> GetUsersByChannelAsync(int channelId);
        Task<IEnumerable<User>> GetUsersByWorkspaceAsync(int workspaceId);
    }
}
