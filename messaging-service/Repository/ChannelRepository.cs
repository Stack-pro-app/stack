using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using System.Threading.Channels;

namespace messaging_service.Repository
{
    public class ChannelRepository:IChannelRepository
    {
        private readonly AppDbContext _context;
        public ChannelRepository(AppDbContext context) { 
            _context = context;
        }

        public bool CreateChannel(Channel channel)
        {
            try
            {
                _context.Channels.Add(channel);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex) { throw; }
        }

        public bool DeleteChannel(int channelId)
        {
            try
            {
                var channel = _context.Channels.FirstOrDefault(x => x.Id == channelId) ?? throw new InvalidOperationException("Invalid Channel");
                _context.Channels.Remove(channel);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex) { throw; }
        }

        public Channel GetChannel(int channelId)
        {
            try
            {
                var channel = _context.Channels.FirstOrDefault(x => x.Id == channelId) ?? throw new InvalidOperationException("Invalid Channel");
                return channel;
            }
            catch (Exception ex) { throw; }
        }

        public IEnumerable<Channel> GetChannelsByBoth(int userId, int workspaceId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Channel> GetChannelsByWorkspace(int workspaceId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateChannel(Channel channel)
        {
            try
            {
                var chnl = _context.Channels.FirstOrDefault(x => x.Id == channelId) ?? throw new InvalidOperationException("Invalid Channel");
                _context.Channels.Remove(channel);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw; }
        }
    }
}
