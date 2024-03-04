using messaging_service.models.domain;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.dto.Minimal
{
    public class ChannelMinimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime? Created_at { get; set; }
        public bool Is_private { get; set; }
    }
}
