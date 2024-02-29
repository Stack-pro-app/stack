using messaging_service.models.dto;

namespace messaging_service.Repository.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(UserDto user);
        bool DeleteUser(int userId);
        bool UpdateUser(UserDto user);
        UserDto GetUser(int userId);
        IEnumerable<UserDto> GetUsersByChannel(int channelId);
        IEnumerable<UserDto> GetUsersByWorkspace(int workspaceId);
    }
}
