using messaging_service.models.domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IWorkspaceRepository
    {
        public Task<bool> CreateWorkspaceAsync(string name);
        public Task<bool> DeleteWorkspaceAsync(int workspaceId);
        public Task<bool> UpdateWorkspaceAsync(int id,string name);
        public Task<Workspace> GetWorkspaceAsync(int workspaceId);
    }
}
