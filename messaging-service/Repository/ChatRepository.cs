using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using messaging_service.Data;
using Microsoft.EntityFrameworkCore;

namespace messaging_service.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateChatAsync(Chat message)
        {
            try
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteChatPartAsync(int messageId)
        {
            try
            {
                var message = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId && x.Is_deleted == false) ?? throw new InvalidOperationException("Message Inexistant");
                message.Is_deleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteChatPermAsync(int messageId)
        {
            try
            {
                var message = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId) ?? throw new InvalidOperationException("Message Inexistant");
                _context.Chats.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Chat>> GetChannelMessageAsync(int channelId)
        {
            try
            {
                var msgs = await _context.Chats.Where(x => x.ChannelId == channelId).ToListAsync();
                return msgs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Chat> GetMessageAsync(int messageId)
        {
            try
            {
                var message = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId) ?? throw new InvalidOperationException("Message Inexistant");
                return message;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateChatAsync(int messageId, string message)
        {
            try
            {
                var msg = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId && x.Is_deleted == false) ?? throw new InvalidOperationException("Message Inexistant");
                msg.Message = message;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}