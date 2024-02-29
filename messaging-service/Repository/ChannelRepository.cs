using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace messaging_service.Repository
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly AppDbContext _context;

        public ChannelRepository(AppDbContext context)
        {
            _context = context;
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
                var channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new InvalidOperationException("Invalid Channel");
                _context.Channels.Remove(channel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<Channel> GetChannelAsync(int channelId)
        {
            try
            {
                var channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new InvalidOperationException("Invalid Channel");
                return channel;
            }
            catch (Exception) { throw; }
        }

        public async Task<IEnumerable<Channel>> GetChannelsByWorkspaceAsync(int workspaceId)
        {
            try
            {
                var channels = await _context.Channels.Where(x => x.WorkspaceId == workspaceId).ToListAsync();
                return channels;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> UpdateChannelAsync(Channel channel)
        {
            try
            {
                _context.Channels.Update(channel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
