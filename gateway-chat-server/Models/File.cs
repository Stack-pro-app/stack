namespace gateway_chat_server.Models
{
    public class FileChunk
    {
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string ChannelString { get; set; }
        public string? Message { get; set; }
        public int? ParentId { get; set; }
        public byte[] ChunkData {  get; set; }
    }
}
