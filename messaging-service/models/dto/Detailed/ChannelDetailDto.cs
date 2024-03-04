using messaging_service.models.dto.Minimal;

namespace messaging_service.models.dto.Detailed
{
    public class ChannelDetailDto
    {
        public int Id { get; set; }
        public string ChannelString { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Created_at { get; set; }
        public bool Is_private { get; set; }
    }
}
