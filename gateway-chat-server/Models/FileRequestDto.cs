namespace gateway_chat_server.Models
{
    public class FileRequestDto
    {
        public string ChannelString { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
