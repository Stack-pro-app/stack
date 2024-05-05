
using AutoMapper;
using messaging_service.models.domain;
using messaging_service.Models.Dto.Others;
using messaging_service.Producer;
using messaging_service.Repository.Interfaces;

namespace messaging_service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQProducer _producer;
        public NotificationService(IWorkspaceRepository workspaceRepository,IChannelRepository channelRepository,IMapper mapper,IRabbitMQProducer producer) 
        {
            _mapper = mapper;
            _workspaceRepository = workspaceRepository;
            _channelRepository = channelRepository;
            _producer = producer;
        }
        public Task SendJoiningChannelNotif(int channelId)
        {
            throw new NotImplementedException();
        }

        public Task SendJoiningWorkspaceNotif(int workspaceId)
        {
            throw new NotImplementedException();
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
                channelId = channel.Id,
                workspaceId = channel.WorkspaceId,
                Message = $"New Messages in {channel.Name}/{workspace}",
                NotificationStrings = notifStrings

            };
            //Send notification
            _producer.SendToQueue(notification,"notification");
        }
    }
}
