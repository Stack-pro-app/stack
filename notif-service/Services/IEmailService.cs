namespace notif_service.Services
{
    public interface IEmailService
    {
        public Task SendEmailType1(string email, string message, string title, string action = "https://google.com");
    }
}
