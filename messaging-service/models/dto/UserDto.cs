namespace messaging_service.models.dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Last_login { get; set; }
        public int WorkspaceId { get; set; }
        public ICollection<int> MembershipsIds { get; set; }
    }
}
