using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;

namespace messaging_service.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(string authId);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserAsync(string authId);
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetUsersByChannelAsync(int channelId);
        Task<IEnumerable<UserDetailDto>> GetUsersByWorkspaceAsync(int workspaceId);
        Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<int> usersId);
        Task RemoveUserFromWorkspace(int workspaceId, int userId);
        Task<IEnumerable<Workspace>> SetLoginAndGetWorkspaces(string authId);
        Task<User> GetUserByEmailAsync(string email);
    }
}
