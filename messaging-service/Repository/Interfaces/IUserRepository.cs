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
        Task<IEnumerable<User>> GetUsersByChannelAsync(int channelId);
        Task<IEnumerable<UserDetailDto>> GetUsersByWorkspaceAsync(int workspaceId);
        Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<int> usersId);
        Task<IEnumerable<string>> AddUsersToWorkspace(int workspaceId, ICollection<string> emails);
        Task<IEnumerable<string>> RemoveUserFromWorkspace(int workspaceId, ICollection<int> usersId);
        Task<IEnumerable<Workspace>> SetLoginAndGetWorkspaces(string authId);
        Task<User> GetUserByEmailAsync(string email);
    }
}
