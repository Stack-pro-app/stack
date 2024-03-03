﻿using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
                Channel channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == channelId) ?? throw new InvalidOperationException("Invalid Channel");
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
                Channel validChannel = await _context.Channels.FirstOrDefaultAsync(c => c.Id == channel.Id) ?? throw new Exception("Invalid Channel");
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
            try
            {
                Channel Channel = await _context.Channels.FirstOrDefaultAsync(c => c.Id == channelId) ?? throw new Exception("Invalid Channel!");
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new Exception("Invalid User!");
                
                Member member = new()
                {
                    ChannelId = channelId,
                    UserId = userId,
                };
                _context.Add(member);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

    }
}