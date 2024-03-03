namespace messaging_service.models.dto.Response
{
    public class ChannelResponseDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Created_at { get; set; }
        public bool Is_private { get; set; }
        public ICollection<object>? Messages { get; set; }
    }
}
