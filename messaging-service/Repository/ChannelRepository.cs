using AutoMapper;
using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Detailed;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using messaging_service.models.dto.Requests;

namespace messaging_service.Repository
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ChannelRepository> _logger;
        public ChannelRepository(AppDbContext context,IMapper mapper, ILogger<ChannelRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateChannelAsync(Channel channel)
        {
                _context.Channels.Add(channel);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteChannelAsync(int channelId)
        {
                Channel channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new ValidationException("Invalid Channel");
                _context.Channels.Remove(channel);
                await _context.SaveChangesAsync();
        }

        public async Task<ChannelDetailDto> GetChannelAsync(int channelId)
        {
                Channel channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new ValidationException("Invalid Channel");
                ChannelDetailDto channelMinimalDto = _mapper.Map<ChannelDetailDto>(channel);
                return channelMinimalDto;
        }

        public async Task<IEnumerable<ChannelMinimalDto>> GetChannelsByWorkspaceAsync(int workspaceId)
        {
                IEnumerable<Channel> channels = await _context.Channels.Where(x => x.WorkspaceId == workspaceId).ToListAsync()?? throw new ValidationException("Invalid Workspace");
                IEnumerable<ChannelMinimalDto> channelsMinimalDto = _mapper.Map<IEnumerable<Channel>, IEnumerable<ChannelMinimalDto>>(channels);
                return channelsMinimalDto;
        }

        public async Task UpdateChannelAsync(Channel channel)
        {
                Channel validChannel = await _context.Channels.FirstOrDefaultAsync(c => c.Id == channel.Id) ?? throw new ValidationException("Invalid Channel");
                if(!channel.Description.IsNullOrEmpty()) validChannel.Description = channel.Description;
                if(!channel.Name.IsNullOrEmpty()) validChannel.Name = channel.Name;
                validChannel.Is_private = channel.Is_private;

                await _context.SaveChangesAsync();
        }

        public async Task AddUserToPrivateChannel(int channelId, int userId)
        {
            var memberVerify = await _context.Members.FirstOrDefaultAsync(x => x.UserId == userId && x.ChannelId == channelId);
            if (memberVerify != null)
            {
                _logger.LogInformation(memberVerify.ToString());
                throw new ValidationException("Already a member");
            }

            var channel = await _context.Channels.FirstOrDefaultAsync(c => c.Id == channelId && c.Is_OneToOne == false) ?? throw new ValidationException("Invalid Channel!");
            _logger.LogInformation(channel.WorkspaceId+" "+userId);
            var userWorkspace = await _context.UsersWorkspaces.FirstOrDefaultAsync(u => u.UserId == userId && u.WorkspaceId == channel.WorkspaceId) ?? throw new ValidationException("Invalid User!");
            _logger.LogInformation(userWorkspace.ToString());

            Member member = new()
            {
                ChannelId = channelId,
                UserId = userId,
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromPrivateChannel(int channelId, int userId)
        {
                Member member = await _context.Members.FirstOrDefaultAsync(m => m.ChannelId == channelId && m.UserId == userId) ?? throw new ValidationException("Invalid Operation");
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
        }

        public async Task<Channel> CreateOneToOneChannel(OneToOneChannelRequest request)
        {
            int u1 = request.User1;
            int u2 = request.User2;
            int workspaceId = request.WorkspaceId;
            var user1 = await _context.Users.FindAsync(u1) ?? throw new ValidationException("Invalid User");
            var validUser1 = await _context.UsersWorkspaces.FirstOrDefaultAsync(uw => uw.UserId == u1 && uw.WorkspaceId == workspaceId) ?? throw new ValidationException("User doesn't belong to workspace");
            var user2 = await _context.Users.FindAsync(u2) ?? throw new ValidationException("Invalid User");
            var validUser2 = await _context.UsersWorkspaces.FirstOrDefaultAsync(uw => uw.UserId == u2 && uw.WorkspaceId == workspaceId) ?? throw new ValidationException("User doesn't belong to workspace");

            Channel channel = new()
            {
                Name = user1.Name + "," + user2.Name,
                Is_private = true,
                Is_OneToOne = true,
                WorkspaceId = workspaceId,
            };

            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();

            Member member1 = new()
            {
                ChannelId = channel.Id,
                UserId = user1.Id,
            };

            Member member2 = new()
            {
                ChannelId = channel.Id,
                UserId = user2.Id,
            };

            _context.Members.Add(member1);
            _context.Members.Add(member2);
            await _context.SaveChangesAsync();

            return channel;
        }

        public async Task<Channel?> GetOneToOneChannel(OneToOneChannelRequest request)
        {
            int u1 = request.User1;
            int u2 = request.User2;
            int workspaceId = request.WorkspaceId;
            var result = await _context.Members.Where(m => m.UserId == u1 || m.UserId == u2).Include(m => m.Channel).Select(m => m.Channel).Where(c => c.Is_OneToOne == true && c.WorkspaceId == workspaceId).Distinct().FirstOrDefaultAsync();
            return result;
        }
    }
}
