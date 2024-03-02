using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int authId);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserAsync(int authId);
        Task<IEnumerable<object>> GetUsersByChannelAsync(int channelId);
        Task<IEnumerable<object>> GetUsersByWorkspaceAsync(int workspaceId);
        Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<int> usersId);
        Task<IEnumerable<string>> RemoveUserFromWorkspace(int workspaceId, ICollection<int> usersId);
    }
}
