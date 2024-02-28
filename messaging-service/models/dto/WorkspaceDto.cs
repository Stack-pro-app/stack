namespace messaging_service.models.dto
{
    public class WorkspaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> ChannelIds { get; set; }
        public ICollection<int> UsersIds { get; set; }
    }
}
