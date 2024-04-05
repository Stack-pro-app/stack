using messaging_service.models.domain;
using messaging_service.models.dto.Others;
using messaging_service.Repository.Interfaces;
using messaging_service.Data;
using Microsoft.EntityFrameworkCore;
using messaging_service.models.dto.Detailed;
using AutoMapper;
using messaging_service.models.dto.Minimal;
using System;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Amazon.S3;
using messaging_service.Producer;
using messaging_service.Models.Dto.Others;

namespace messaging_service.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAmazonS3 _S3client;
        public ChatRepository(AppDbContext context,IMapper mapper, IAmazonS3 client
            )
        {
            _context = context;
            _mapper = mapper;
            _S3client=client;
        }

        public async Task CreateChatAsync(Chat message)
        {
                _context.Chats.Add(message);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteChatPartAsync(int messageId)
        {
                var message = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId && x.Is_deleted == false) ?? throw new ValidationException("Message Inexistant");
                message.Is_deleted = true;
                await _context.SaveChangesAsync();
        }

        public async Task DeleteChatPermAsync(int messageId)
        {
                Chat message = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageId) ?? throw new ValidationException("Message Inexistant");
               await _S3client.DeleteObjectAsync("stack-messaging-service", message.Attachement_Key);
                _context.Chats.Remove(message);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteChatPermAsync(Guid messageId)
        {
                Chat message = await _context.Chats.FirstOrDefaultAsync(x => x.MessageId == messageId) ?? throw new ValidationException("Message Inexistant");
                await _S3client.DeleteObjectAsync("stack-messaging-service", message.Attachement_Key);
                _context.Chats.Remove(message);
                await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageDetailDto>> GetChannelLastMessagesAsync(int channelId,int page)
        {
                if (page < 1) throw new Exception("Invalid Page");
                int position = (page - 1) * 20;
                IEnumerable<Chat> msgs = await _context.Chats.Where(x => x.ChannelId == channelId && x.Is_deleted == false).OrderByDescending(m => m.Created_at).Include(m=>m.User).Include(m => m.Parent).Skip(position).Take(20).ToListAsync();
                if (msgs.IsNullOrEmpty()) return new List<MessageDetailDto>();
                IEnumerable<MessageDetailDto> messageDetailDtos = _mapper.Map<IEnumerable<Chat>, IEnumerable<MessageDetailDto>>(msgs);

                return messageDetailDtos;
        }


        public async Task<MessageDetailDto> GetMessageAsync(int messageId)
        {
                Chat message = await _context.Chats.Include(m => m.User).Include(m => m.Parent).FirstOrDefaultAsync(x => x.Id == messageId && x.Is_deleted == false) ?? throw new ValidationException("Message Inexistant");
                MessageDetailDto messageDetailDto = _mapper.Map<MessageDetailDto>(message);
                return messageDetailDto;
        }

        public async Task UpdateChatAsync(Guid messageId, string message)
        {
                var msg = await _context.Chats.FirstOrDefaultAsync(x => x.MessageId == messageId && x.Is_deleted == false) ?? throw new ValidationException("Message Inexistant");
                msg.Message = message;
                msg.Modified_at = DateTime.Now;
                await _context.SaveChangesAsync();
        }
    }
}