using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;

namespace messaging_service.Repository.Interfaces
{
    public interface IWorkspaceRepository
    {
        public Task CreateWorkspaceAsync(Workspace workspace);
        public Task DeleteWorkspaceAsync(int workspaceId);
        public Task UpdateWorkspaceAsync(int id,string name);
        public Task<WorkspaceDetailDto> GetWorkspaceAsync(int workspaceId, int userId);
        public Task<string> GetWorkspaceName(int workspaceId);
        public Task<bool> VerifyAdminStatus(string authId, int workspaceId);
        public Task<bool> VerifyMembershipWorkspace(string authId, int workspaceId);
        public Task<bool> VerifyAdminStatusV2(int userId, int workspaceId);
        public Task<List<string>> GetNotifStringsWorkspace(int workspaceId);
    }
}
