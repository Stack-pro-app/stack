using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;

namespace messaging_service.Repository.Interfaces
{
    public interface IWorkspaceRepository
    {
        public Task CreateWorkspaceAsync(string name,int userId);
        public Task DeleteWorkspaceAsync(int workspaceId);
        public Task UpdateWorkspaceAsync(int id,string name);
        public Task<WorkspaceDetailDto> GetWorkspaceAsync(int workspaceId, int userId);
        public Task<string> GetWorkspaceName(int workspaceId);
    }
}
