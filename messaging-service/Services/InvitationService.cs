using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.Models.Domain;
using messaging_service.Models.Dto.Others;
using messaging_service.Models.Dto.Requests;
using messaging_service.Repository.Interfaces;

namespace messaging_service.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public InvitationService(IInvitationRepository invitationRepository, IMapper mapper, INotificationService notificationService, 
            IUserRepository userRepository, IWorkspaceRepository workspaceRepository)
        {
            _invitationRepository = invitationRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task AcceptInvistation(string token)
        {
            await _invitationRepository.AcceptInvitation(token);
            
        }

        public async Task DeclineInvitation(string token)
        {
            await _invitationRepository.DeleteInvitation(token);
        }

        public async Task<IEnumerable<Invitation>> GetAllInvitations(int userId)
        {
            return await _invitationRepository.GetInvitations(userId);
        }

        public async Task SendInvitation(InvitationRequestDto inv)
        {
            //Create Invitation And Store It
            Invitation invitation = _mapper.Map<Invitation>(inv);
            await _invitationRepository.CreateInvitation(invitation);

            //Get User and Workspace
            User user = await _userRepository.GetUserAsync(invitation.UserId);
            WorkspaceDetailDto workspace = await _workspaceRepository.GetWorkspaceAsync(invitation.WorkspaceId, user.Id);



            //Send Notification
            NotificationDto notif = new()
            {
                MailTo = user.Email,
                Title = "Invitation To " + workspace.Name,
                Message = "Dear " + user.Name + ", You have been invited to join our workspace" + workspace.Name,
                NotificationStrings = new List<string>(),
                workspaceId = workspace.Id,
                Links = new Dictionary<string, string>()
            };
            notif.Links.Add("Accept", "http://" + Environment.GetEnvironmentVariable("DOMAIN") + ":" + Environment.GetEnvironmentVariable("PORT")
            + "/api/invitation/accept/" + invitation.Id);
            notif.Links.Add("Decline", "http://" + Environment.GetEnvironmentVariable("DOMAIN") + ":" + Environment.GetEnvironmentVariable("PORT")
                + "/api/invitation/decline/" + invitation.Id);
            notif.NotificationStrings.Add(user.NotificationString);
            _notificationService.SendNotification(notif);
        }
    }
}
