namespace notif_service.Models
{
    public class EmailDto
    {
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string MailTo { get; set; } = null!;
        public Dictionary<string,string>? Links { get; set; } = new Dictionary<string, string>();
    }
}
