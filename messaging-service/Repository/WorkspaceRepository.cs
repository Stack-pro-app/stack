using messaging_service.Data;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto;
using messaging_service.models.dto.Minimal;
using messaging_service.Repository.Interfaces;
using messaging_service.models.domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace messaging_service.Repository
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public WorkspaceRepository(AppDbContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateWorkspaceAsync(string name,int adminId)
        {
            try
            {
                var result = await _context.Users.FirstOrDefaultAsync(u=> u.Id == adminId);
                if (result == null) throw new ValidationException("Invalid User");
                if (name.IsNullOrEmpty()) throw new ValidationException("Invalid Name");
                Workspace workspace = new();
                workspace.Name = name;
                workspace.AdminId = adminId;
                _context.Workspaces.Add(workspace);
                await _context.SaveChangesAsync();
                UserWorkspace userWorkspace = new()
                {
                    UserId = adminId,
                    WorkspaceId = workspace.Id,
                };
                _context.UsersWorkspaces.Add(userWorkspace);
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteWorkspaceAsync(int workspaceId)
        {
            try
            {
                if (workspaceId < 0) throw new ValidationException("Invalid Workspace Id");
                var workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == workspaceId)?? throw new ValidationException("Invalid Workspace");
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

        public async Task<WorkspaceDetailDto> GetWorkspaceAsync(int workspaceId,int userId)
        {
            try
            {
                Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == workspaceId)?? throw new ValidationException("invalid Workspace");
                IEnumerable<Channel> channel = await _context.Channels.Where(x => x.WorkspaceId == workspaceId).ToListAsync() ?? throw new ValidationException("invalid Channels");
                //Get the detailed main channel
                Channel mainChannel = channel.FirstOrDefault(c => c.Name == "main") ?? throw new ValidationException("Invalid Workspace");
                ChannelDetailDto mainDetail = _mapper.Map<ChannelDetailDto>(mainChannel);
                //Get the minimal public channels
                IEnumerable<Channel> publicChannels = channel.Where(c=>c.Is_private == false && c.Name != "main").ToList();
                IEnumerable<ChannelMinimalDto> publicMinimal = _mapper.Map<IEnumerable<Channel>,IEnumerable<ChannelMinimalDto>>(publicChannels);
                //Get the minimal Private Channels
                IEnumerable<Channel> privateChannels = channel.Where(c => c.Is_private == true).ToList();
                IEnumerable<ChannelMinimalDto> privateMinimal = _mapper.Map<IEnumerable<Channel>, IEnumerable<ChannelMinimalDto>>(privateChannels);
                WorkspaceDetailDto detail = new()
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    adminId = workspace.AdminId,
                    MainChannel = mainDetail,
                    PublicChannels = publicMinimal.ToList(),
                    PrivateChannels = privateMinimal.ToList(),

                };
                return detail;
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
                Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == id) ?? throw new ValidationException("Can't Find User");
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
