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
        /// <summary>
        /// Get Unseen Notifications , once you get them they become seen
        /// </summary>
        [HttpGet("Unseen/{notificationString}")]
        public async Task<ActionResult<ResponseDto>> GetUnseenNotifications([FromRoute] string notificationString, [FromQuery] int page)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                var notifications = await _notificationService.GetUnseenNotificationsAsync(notificationString,page);
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
        /// <summary>
        /// Get old and seen Notifications by pages of 20
        /// </summary>
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
        /// <summary>
        /// Sets the all notifications of a user to seen (no need to use it for now)
        /// </summary>
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
        /// <summary>
        /// Used To add a notification via Rest Api (used for 3rd party integration)
        /// </summary>
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
