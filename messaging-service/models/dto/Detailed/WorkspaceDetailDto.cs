using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Minimal;

namespace messaging_service.models.dto.Detailed
{
    public class WorkspaceDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChannelDetailDto MainChannel { get; set; }
        public List<ChannelMinimalDto>? Channels { get; set; }
    }
}
