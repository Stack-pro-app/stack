using messaging_service.models.domain;
using messaging_service.Repository.Interfaces;
using messaging_service.Data;

namespace messaging_service.Repository
{
    
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        public ChatRepository(AppDbContext appDbContext) { _context=appDbContext }
        public bool CreateChat(Chat message)
        {
            try
            {
                _context.Add(message);
                _context.SaveChanges();
                return true;
            }
            catch
            (Exception ex)
            { throw; }
        }
        //Delete Partially
        public bool DeleteChatPart(int messageId)
        {
            try
            {
                var message = _context.Chats.FirstOrDefault(x => x.Id == messageId && x.Is_deleted == false) ?? throw new InvalidOperationException("Message Inexistant");
                message.Is_deleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex){ throw; }
        }
        //Delete Permanently
        public bool DeleteChatPerm(int messageId)
        {
            try
            {
                var message = _context.Chats.FirstOrDefault(x => x.Id == messageId) ?? throw new InvalidOperationException("Message Inexistant");
                _context.Chats.Remove(message);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw; }
        }



        public IEnumerable<Chat> GetChannelMessage(int channelId)
        {
            try
            {
                var msgs = _context.Chats.Where(x => x.ChannelId == channelId).ToList();
                return msgs;
            }
            catch (Exception ex) { throw; }
        }

        public Chat GetMessage(int messageId)
        {
            try
            {
                var message = _context.Chats.FirstOrDefault(x => x.Id == messageId) ?? throw new InvalidOperationException("Message Inexistant");
                return message;
            }
            catch (Exception ex) { throw; }
        }

        public bool UpdateChat(Chat msg)
        {
            try
            {
                _context.Chats.Update(msg);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex) { throw; } 
        }
    }
}
