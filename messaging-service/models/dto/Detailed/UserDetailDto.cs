using messaging_service.models.domain;

namespace messaging_service.models.dto.Detailed
{
    public class UserDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Last_login { get; set; }
        public string AuthId { get; set; }
        public string NotificationString { get; set; }
    }
}
