namespace messaging_service.models.dto
{
    public class ChannelUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Is_private { get; set; }
    }
}
