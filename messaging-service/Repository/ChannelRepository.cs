using AutoMapper;
using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Detailed;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

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

        public async Task<bool> CreateChannelAsync(Channel channel)
        {
            try
            {
                _context.Channels.Add(channel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> DeleteChannelAsync(int channelId)
        {
            try
            {
                Channel channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new ValidationException("Invalid Channel");
                _context.Channels.Remove(channel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<ChannelDetailDto> GetChannelAsync(int channelId)
        {
            try
            {
                Channel channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new ValidationException("Invalid Channel");
                ChannelDetailDto channelMinimalDto = _mapper.Map<ChannelDetailDto>(channel);
                return channelMinimalDto;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<ChannelMinimalDto>> GetChannelsByWorkspaceAsync(int workspaceId)
        {
            try
            {
                IEnumerable<Channel> channels = await _context.Channels.Where(x => x.WorkspaceId == workspaceId).ToListAsync()?? throw new ValidationException("Invalid Workspace");
                IEnumerable<ChannelMinimalDto> channelsMinimalDto = _mapper.Map<IEnumerable<Channel>, IEnumerable<ChannelMinimalDto>>(channels);
                return channelsMinimalDto;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> UpdateChannelAsync(Channel channel)
        {
            try
            {
                Channel validChannel = await _context.Channels.FirstOrDefaultAsync(c => c.Id == channel.Id) ?? throw new ValidationException("Invalid Channel");
                if(!channel.Description.IsNullOrEmpty()) validChannel.Description = channel.Description;
                if(!channel.Name.IsNullOrEmpty()) validChannel.Name = channel.Name;
                if(channel.Is_private != null) validChannel.Is_private = channel.Is_private;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> AddUserToPrivateChannel(int channelId, int userId)
        {
            var memberVerify = await _context.Members.FirstOrDefaultAsync(x => x.UserId == userId && x.ChannelId == channelId);
            if (memberVerify != null)
            {
                _logger.LogInformation(memberVerify.ToString());
                throw new ValidationException("Already a member");
            }

            var channel = await _context.Channels.FirstOrDefaultAsync(c => c.Id == channelId) ?? throw new ValidationException("Invalid Channel!");
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
            return true;
        }

        public async Task<bool> RemoveUserFromPrivateChannel(int channelId, int userId)
        {
            try
            {
                Member member = await _context.Members.FirstOrDefaultAsync(m => m.ChannelId == channelId && m.UserId == userId) ?? throw new ValidationException("Invalid Operation");
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

    }
}
