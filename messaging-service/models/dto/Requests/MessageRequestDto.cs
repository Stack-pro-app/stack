using messaging_service.models.domain;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.dto.Requests
{
    public class MessageRequestDto
    {
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string Message { get; set; }
        public int? ParentId { get; set; }
    }

    public class MessageUpdateDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }


}
