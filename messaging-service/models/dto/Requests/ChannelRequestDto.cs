using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.dto.Requests
{
    public class ChannelRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Is_private { get; set; }
        public int WorkspaceId { get; set; }
    }

    public class OneToOneChannelRequest
    {
        public int User1 {  get; set; }
        public int User2 { get; set; }

        public int WorkspaceId { get; set; }
    }

    public class ChannelUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Is_private { get; set; }
    }
}
