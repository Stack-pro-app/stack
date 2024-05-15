
using AutoMapper;
using messaging_service.models.domain;
using messaging_service.Models.Dto.Others;
using messaging_service.Producer;
using messaging_service.Repository.Interfaces;
using System.Threading.Channels;

namespace messaging_service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationService> _logger;
        private readonly IRabbitMQProducer _producer;
        public NotificationService(IWorkspaceRepository workspaceRepository,IChannelRepository channelRepository,IMapper mapper,IRabbitMQProducer producer
            ,ILogger<NotificationService> logger,IUserRepository ur) 
        {
            _mapper = mapper;
            _workspaceRepository = workspaceRepository;
            _channelRepository = channelRepository;
            _producer = producer;
            _logger = logger;
            _userRepository = ur;
        }

        public void SendNotification(NotificationDto notif)
        {
            _logger.LogInformation("Sending Notification to RabbitMQ");
            _producer.SendToQueue(notif,"notification");
        }

        public Task SendJoiningChannelNotif(int channelId)
        {
            throw new NotImplementedException();
        }

        public async Task SendJoiningWorkspaceNotif(int userId,int workspaceId)
        {
            var workspace = await _workspaceRepository.GetWorkspaceName(workspaceId);
            _logger.LogInformation("getworkpsacename working "+workspace);
            var user = await _userRepository.GetUserAsync(userId);
            _logger.LogInformation("getuser working "+user.Name);
            List<string> notifStrings = await _workspaceRepository.GetNotifStringsWorkspace(workspaceId);
            _logger.LogInformation("getnotifstrings working "+notifStrings);
            NotificationDto notification = new()
            {
                Title = "New Co-Worker",
                channelId = null,
                workspaceId = workspaceId,
                Message = $"{user.Name} Just Joined {workspace}",
                NotificationStrings = notifStrings
            };
            _logger.LogInformation("notification created");
            //Send notification
            SendNotification(notification);
            _logger.LogInformation("notification sent");
        }

        public async Task SendMessageNotif(int channelId)
        {
            //Find Channel Info
            var channel = await _channelRepository.GetChannelAsync(channelId);
            var workspace = await _workspaceRepository.GetWorkspaceName(channel.WorkspaceId);
            List<string> notifStrings = await _channelRepository.GetChannelNotificationStrings(channelId);
            //Create notification
            NotificationDto notification = new()
            {
                Title = "New Message",
                channelId = channel.Id,
                workspaceId = channel.WorkspaceId,
                Message = $"New Messages in {workspace}/{channel.Name}",
                NotificationStrings = notifStrings
            };
            //Send notification
            SendNotification(notification);
        }
    }
}
