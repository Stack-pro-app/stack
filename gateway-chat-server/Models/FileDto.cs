namespace gateway_chat_server.Models
{
    public class FileDto
    {
        public string ChannelString { get; set; }
        public int UserId { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
