using messaging_service.Data;
using messaging_service.models.dto;
using messaging_service.Repository.Interfaces;
using messaging_service.models.domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.Repository
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly AppDbContext _context;
        public WorkspaceRepository(AppDbContext context) {
            _context = context;
        }
        public async Task<bool> CreateWorkspaceAsync(string name,int adminId)
        {
            try
            {
                Workspace workspace = new();
                workspace.Name = name;
                workspace.AdminId = adminId;
                _context.Workspaces.Add(workspace);
                await _context.SaveChangesAsync();
                //creating main channel
                Channel main = new(){
                    Name = "main",
                    Description = "The Workspace Main Room",
                    Is_private = false,
                    WorkspaceId = workspace.Id,
                };
                _context.Channels.Add(main);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating workspace: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteWorkspaceAsync(int workspaceId)
        {
            try
            {
                var workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == workspaceId)?? throw new InvalidOperationException("invalid Workspace");
                _context.Workspaces.Remove(workspace);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting workspace: {ex.Message}");
                return false;
            }
        }

        public async Task<Workspace> GetWorkspaceAsync(int workspaceId)
        {
            try
            {
                var workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == workspaceId) ?? throw new InvalidOperationException("invalid Workspace");
                return workspace;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding workspace: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateWorkspaceAsync(int id,string name)
        {
            try
            {
                Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == id) ?? throw new InvalidOperationException("Can't Find User");
                workspace.Name = name;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating workspace: {ex.Message}");
                return false;
            }
        }
    }
}
