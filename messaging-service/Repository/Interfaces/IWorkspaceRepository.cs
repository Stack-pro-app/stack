using messaging_service.models.dto;

namespace messaging_service.Repository.Interfaces
{
    public interface IWorkspaceRepository
    {
        bool CreateWorkspace(WorkspaceDto workspace);
        bool DeleteWorkspace(int workspaceId);
        bool UpdateWorkspace(WorkspaceDto workspace);
        WorkspaceDto GetWorkspace(int workspaceId);
    }
}
