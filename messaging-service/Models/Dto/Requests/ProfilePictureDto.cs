namespace messaging_service.Models.Dto.Requests
{
    public class ProfilePictureDto
    {
        public string authId { get; set; } = null!;
        public IFormFile picture { get; set; }=null!;
    }
}
