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
using messaging_service.Producer;
using messaging_service.Models.Dto.Others;

namespace messaging_service.Repository
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRabbitMQProducer _producer;
        public WorkspaceRepository(AppDbContext context,IMapper mapper,IRabbitMQProducer producer) {
            _context = context;
            _mapper = mapper;
            _producer = producer;
        }
        public async Task CreateWorkspaceAsync(Workspace workspace)
        {

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == workspace.AdminId) ?? throw new ValidationException("Invalid User");
                _context.Workspaces.Add(workspace);
                await _context.SaveChangesAsync();
                UserWorkspace userWorkspace = new()
                {
                    UserId = workspace.AdminId,
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
                PMWorkspaceDto PMworkspace = new()
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    AdminId = user.AuthId,
                };
                _producer.SendToQueue(PMworkspace, "workspace");
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
                IEnumerable<Channel> publicChannels = channel.Where(c =>c.Name != "main" && c.Is_private == false).ToList();
                IEnumerable<Channel> authorizedChannels;
                if (await VerifyAdminStatusV2(userId, workspaceId))
                {
                    authorizedChannels = await _context.Channels.Where(c => c.Is_OneToOne == false && c.Is_private == true).ToListAsync();
                }
                else
                {
                    authorizedChannels = await _context.Members.Where(m => m.UserId == userId).Include(m => m.Channel).Select(m => m.Channel).Where(c => c.Is_OneToOne == false).ToListAsync();
                }
                IEnumerable<Channel> channels = publicChannels.Concat(authorizedChannels);
                IEnumerable<ChannelMinimalDto> minimalChannels = _mapper.Map<IEnumerable<Channel>, IEnumerable<ChannelMinimalDto>>(channels);
                WorkspaceDetailDto detail = new()
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    MainChannel = mainDetail,
                    Channels = minimalChannels.ToList(),

                };
                return detail;
        }

        public async Task<string> GetWorkspaceName(int workspaceId)
        {
            var result = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId) ?? throw new ValidationException("Invalid Workspace");
            return result.Name;
        }

        public async Task UpdateWorkspaceAsync(int id,string name)
        {
                Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(w => w.Id == id) ?? throw new ValidationException("Can't Find User");
                workspace.Name = name;
                await _context.SaveChangesAsync();

        }
        public async Task<bool> VerifyAdminStatus(string authId, int workspaceId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId);
            if (user == null) return false;

            return await VerifyAdminStatusV2(user.Id, workspaceId);
        }

        public async Task<bool> VerifyAdminStatusV2(int userId, int workspaceId)
        {
            return await _context.Workspaces.AnyAsync(w => w.Id == workspaceId && w.Admin.Id == userId);
        }

        public async Task<bool> VerifyMembershipWorkspace(string authId, int workspaceId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.AuthId == authId);
            if (user == null) return false;

            return await _context.UsersWorkspaces.AnyAsync(uw => uw.UserId == user.Id && uw.WorkspaceId == workspaceId);
        }
    }
}
