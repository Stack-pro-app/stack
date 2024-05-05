namespace messaging_service.models.dto.Minimal
{
    public class WorkspaceMinimalDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created_at { get; set; }
        public int AdminId { get; set; }
    }
}
