using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using notif_service.Models;
using notif_service.Services;

namespace notif_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IMailService _emailService;
        private readonly ILogger<NotificationController> _logger;
        public NotificationController(INotificationService notificationService,IMapper mapper,IMailService emailService,ILogger<NotificationController> logger) {
            _notificationService = notificationService;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
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
                MailRequest mailRequest = new()
                {
                    ToEmail = notificationDto.MailTo,
                    Body = notificationDto.Message,
                    Subject = notificationDto.Title
                };
                _logger.LogInformation(mailRequest.ToString());
                await _emailService.SendEmailAsync(mailRequest);

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


    }
}
