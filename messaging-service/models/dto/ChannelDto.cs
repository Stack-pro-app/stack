using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.dto
{
    public class ChannelDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Is_private { get; set; }
        public int WorkspaceId { get; set; }
    }
}
