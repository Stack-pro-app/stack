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
        public async Task CreateWorkspaceAsync(string name,int adminId)
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
        }

        public async Task DeleteWorkspaceAsync(int workspaceId)
        {
                if (workspaceId < 0) throw new ValidationException("Invalid Workspace Id");
                var workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == workspaceId)?? throw new ValidationException("Invalid Workspace");
                _context.Workspaces.Remove(workspace);
                await _context.SaveChangesAsync();
        }

        public async Task<WorkspaceDetailDto> GetWorkspaceAsync(int workspaceId,int userId)
        {
                Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == workspaceId)?? throw new ValidationException("invalid Workspace");
                IEnumerable<Channel> channel = await _context.Channels.Where(x => x.WorkspaceId == workspaceId).ToListAsync() ?? throw new ValidationException("invalid Channels");
                Channel mainChannel = channel.FirstOrDefault(c => c.Name == "main") ?? throw new ValidationException("Invalid Workspace");
                ChannelDetailDto mainDetail = _mapper.Map<ChannelDetailDto>(mainChannel);
                IEnumerable<Channel> publicChannels = channel.Where(c =>c.Name != "main").ToList();
                IEnumerable<Channel> authorizedChannels = await _context.Members.Where(m => m.UserId == userId).Include(m => m.Channel).Select(m => m.Channel).Where(c=>c.Is_OneToOne == false).ToListAsync();
                IEnumerable<Channel> channels = publicChannels.Concat(authorizedChannels);
                IEnumerable<ChannelMinimalDto> minimalChannels = _mapper.Map<IEnumerable<Channel>, IEnumerable<ChannelMinimalDto>>(channels);
                WorkspaceDetailDto detail = new()
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    adminId = workspace.AdminId,
                    MainChannel = mainDetail,
                    Channels = minimalChannels.ToList(),

                };
                return detail;
        }

        public async Task UpdateWorkspaceAsync(int id,string name)
        {
                Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == id) ?? throw new ValidationException("Can't Find User");
                workspace.Name = name;
                await _context.SaveChangesAsync();

        }
    }
}
