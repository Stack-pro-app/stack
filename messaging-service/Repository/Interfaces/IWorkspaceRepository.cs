using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;

namespace messaging_service.Repository.Interfaces
{
    public interface IWorkspaceRepository
    {
        public Task<bool> CreateWorkspaceAsync(string name,int userId);
        public Task<bool> DeleteWorkspaceAsync(int workspaceId);
        public Task<bool> UpdateWorkspaceAsync(int id,string name);
        public Task<WorkspaceDetailDto> GetWorkspaceAsync(int workspaceId, int userId);
    }
}
