using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using notif_service.Models;
using notif_service.Services;
using System.Net;
using System.Net.Mail;

namespace notif_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;
        IEmailService _emailService;
        public NotificationController(INotificationService notificationService,IMapper mapper,ILogger<NotificationController> logger,IEmailService emailService) {
            _notificationService = notificationService;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }
        [HttpGet("Unseen/{authId}")]
        public async Task<ActionResult<ResponseDto>> GetUnseenNotifications([FromRoute] string authId)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                var notifications = await _notificationService.GetUnseenNotificationsAsync(authId);
                response.Result = notifications;
                response.IsSuccess = true;
                response.Message = "You Have Unseen Notifications!";
                return Ok(response);
            }
            catch(Exception) { 
                response.IsSuccess = false;
                response.Message = "Failed to find new notifications!";
                return BadRequest(response);
            }
        }

        [HttpGet("Seen/{authId}")]
        public async Task<ActionResult<ResponseDto>> GetseenNotifications([FromRoute] string authId, [FromQuery]int page)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                var notifications = await _notificationService.GetMoreNotificationsAsync(authId, page);
                response.Result = notifications;
                response.IsSuccess = true;
                response.Message = "Old Notifications!";
                return Ok(response);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Failed to find old notifications!";
                return BadRequest(response);
            }
        }

        [HttpPut("SetSeen/{authId}")]
        public async Task<ActionResult<ResponseDto>> SetSeenNotifications([FromRoute] string authId)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                await _notificationService.SetNotificationsSeenAsync(authId);
                response.IsSuccess = true;
                response.Message = "Notifications Seen!";
                return Ok(response);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Failed to see notifications!";
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddNotification([FromBody]NotificationDtoV2 notificationDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                //Notification notification = _mapper.Map<Notification>(notificationDto);
                //await _notificationService.AddNotificationAsync(notification);

                var message = new Message(new string[] { "redtius@gmail.com" }, "Test email", "This is the content from our email.");
                //await _emailService.SendEmail(message);

                response.IsSuccess = true;
                response.Message = "Notifications Added!";
                return Ok(response);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Failed to add notifications!";
                return BadRequest(response);
            }
        }

        [HttpPost("SendMail")]
        public async Task<ActionResult<ResponseDto>> SendMail([FromBody]MailData mailData)
        {
            try
            {
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("44436faf1fdd17", "53adac813a5a1f"),
                    EnableSsl = true
                };
                client.Send("from@example.com", "redtius@gmail.com", "Hello world", "testbody");
                return Ok();
            }
            catch(Exception) { return BadRequest(); }
        }


    }
}
