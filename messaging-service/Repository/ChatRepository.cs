using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using messaging_service.Data;
using Microsoft.EntityFrameworkCore;
using messaging_service.models.dto.Detailed;
using AutoMapper;
using messaging_service.models.dto.Minimal;
using System;
using Microsoft.IdentityModel.Tokens;

namespace messaging_service.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ChatRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                throw new Exception("Failed to create chat message.", ex);
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
                throw new Exception("Failed to delete chat message partially.", ex);
            }
        }

        public async Task<bool> DeleteChatPermAsync(int messageId)
        {
            try
            {
                Chat message = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId) ?? throw new InvalidOperationException("Message Inexistant");
                _context.Chats.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete chat message permenantly.", ex);
            }
        }

        public async Task<IEnumerable<MessageDetailDto>> GetChannelLastMessagesAsync(int channelId,int page)
        {
            try
            {
                if (page < 1) throw new Exception("Invalid Page");
                int position = (page - 1) * 20;
                IEnumerable<Chat> msgs = await _context.Chats.Where(x => x.ChannelId == channelId && x.Is_deleted == false).OrderByDescending(m => m.Created_at).Include(m=>m.User).Include(m => m.Parent).Skip(position).Take(20).ToListAsync();
                if (msgs.IsNullOrEmpty()) return new List<MessageDetailDto>();
                IEnumerable<MessageDetailDto> messageDetailDtos = _mapper.Map<IEnumerable<Chat>, IEnumerable<MessageDetailDto>>(msgs);

                return messageDetailDtos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<MessageDetailDto> GetMessageAsync(int messageId)
        {
            try
            {
                Chat message = await _context.Chats.Include(m => m.User).Include(m => m.Parent).FirstOrDefaultAsync(x => x.Id == messageId && x.Is_deleted == false) ?? throw new InvalidOperationException("Message Inexistant");
                MessageDetailDto messageDetailDto = _mapper.Map<MessageDetailDto>(message);
                return messageDetailDto;
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
                msg.Modified_at = DateTime.Now;
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