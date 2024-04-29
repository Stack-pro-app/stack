using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public NotificationController(INotificationService notificationService,IMapper mapper) {
            _notificationService = notificationService;
            _mapper = mapper;
        }
        [HttpGet("Unseen/{notificationString}")]
        public async Task<ActionResult<ResponseDto>> GetUnseenNotifications([FromRoute] string notificationString)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                var notifications = await _notificationService.GetUnseenNotificationsAsync(notificationString);
                List<NotificationDto> notificationsDto = notifications.Select(n => _mapper.Map<NotificationDto>(n)).ToList();
                response.Result = notificationsDto;
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

        [HttpGet("Seen/{notificationString}")]
        public async Task<ActionResult<ResponseDto>> GetseenNotifications([FromRoute] string notificationString, [FromQuery]int page)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                var notifications = await _notificationService.GetMoreNotificationsAsync(notificationString, page);
                List<NotificationDto> notificationsDto = notifications.Select(n => _mapper.Map<NotificationDto>(n)).ToList();
                response.Result = notificationsDto;
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

        [HttpPut("SetSeen/{notificationString}")]
        public async Task<ActionResult<ResponseDto>> SetSeenNotifications([FromRoute] string notificationString)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                await _notificationService.SetNotificationsSeenAsync(notificationString);
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
                Notification notif = _mapper.Map<Notification>(notificationDto);
                await _notificationService.AddNotificationAsync(notif);

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
