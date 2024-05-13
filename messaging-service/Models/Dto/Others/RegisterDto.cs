namespace messaging_service.Models.Dto.Others
{
    public class RegisterDto
    {
        public string ID { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
