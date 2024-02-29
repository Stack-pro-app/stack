using messaging_service.Data;
using messaging_service.models.dto;
using messaging_service.Repository.Interfaces;
using messaging_service.models.domain;

namespace messaging_service.Repository
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly AppDbContext _context;
        public WorkspaceRepository(AppDbContext context) {
            _context = context;
        }
        public bool CreateWorkspace(WorkspaceDto workspaceDto)
        {
            Workspace workspace = new Workspace
            {
                Name = workspaceDto.Name,
            };
            try
            {
                _context.Workspaces.Add(workspace);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating workspace: {ex.Message}");
                return false;
            }
            

        }

        public bool DeleteWorkspace(int workspaceId)
        {
            throw new NotImplementedException();
        }

        public WorkspaceDto GetWorkspace(int workspaceId)
        {
            Workspace workspace = _context.Workspaces.FirstOrDefault(x => x.Id == workspaceId);
            if (workspace == null) { return new WorkspaceDto(); }
        }

        public bool UpdateWorkspace(WorkspaceDto workspace)
        {
            throw new NotImplementedException();
        }
    }
}
