using messaging_service.models.dto.Response;
using messaging_service.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using messaging_service.Services;
using messaging_service.Models.Domain;
using messaging_service.Models.Dto.Detailed;

namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService service, 
            IMapper mapper)
        {
            _invitationService = service;
            _mapper = mapper;

        }
        // This Api Creates an invite and sends a notification to the user, an email also!
        /// <summary>
        /// Creates a new Invitation and sends a notification to the users (with email notification).
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateInvitation([FromBody] InvitationRequestDto invitationDto)
        {
            try
            {
                await _invitationService.SendInvitation(invitationDto);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Invitation Successfully Created!",
                    Result = null,
                };
                return Ok(response);
            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Create Invitation!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Accepts a generated Invite and adds the user to the workspace.
        /// </summary>
        [HttpPost("accept/{token}")]
        public async Task<ActionResult<ResponseDto>> AcceptInvitation([FromRoute] string token, [FromQuery] bool isEmail) 
        {
            try
            {
                await _invitationService.AcceptInvistation(token);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Invitation Successfully Accepted!",
                    Result = null,
                };
                if(isEmail)
                return Redirect("http://" + Environment.GetEnvironmentVariable("DOMAIN") + "/home/"); // Redirect to homepage
                return Ok(response);
            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Accept Invitation!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Declines a generated Invite and removes it.
        /// </summary>
        [HttpPost("decline/{token}")]
        public async Task<ActionResult<ResponseDto>> DeclineInvitation([FromRoute] string token, [FromQuery] bool isEmail)
        {
            try
            {
                await _invitationService.DeclineInvitation(token);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Invitation Successfully Declined!",
                    Result = null,
                };
                if(isEmail)
                return Redirect("http://" + Environment.GetEnvironmentVariable("DOMAIN") + "/home/"); // Redirect to homepage
                return Ok(response);
            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Decline Invitation!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Used to get all the invitations for a user by the messaging service user Id.
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDto>> GetAllInvitations([FromRoute] int userId)
        {
            try
            {
                IEnumerable<Invitation> invitations = await _invitationService.GetAllInvitations(userId);
                IEnumerable<InvitationDetailDto> invs = invitations.Select(_mapper.Map<InvitationDetailDto>);

                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Invitations Successfully Fetched!",
                    Result = invs,
                };
                return Ok(response);
            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Fetch Invitations!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }   

    }
}
