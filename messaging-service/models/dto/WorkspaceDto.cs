namespace messaging_service.models.dto
{
    public class WorkspaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChannelDto? MainChannel { get; set; }
        public ICollection<int>? channelIds { get; set; }
        public ICollection<int>? userIds { get; set; }
    }
}
