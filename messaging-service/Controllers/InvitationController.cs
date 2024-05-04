using messaging_service.models.dto.Response;
using messaging_service.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using messaging_service.Services;
using messaging_service.Models.Domain;
using messaging_service.Models.Dto.Detailed;

namespace messaging_service.Controllers
{
    /// <summary>
    /// This is Invitations Controller
    /// </summary>
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
        // This Api Accepts an invite
        [HttpPut("accept/{token}")]
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
        // This Api Declines an invite
        [HttpPut("decline/{token}")]
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

        // This Api Gets all invites for a user by user id

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDto>> GetAllInvitations([FromRoute] int userId)
        {
            try
            {
                IEnumerable<Invitation> invitations = await _invitationService.GetAllInvitations(userId);
                IEnumerable<InvitationDetailDto> invs = invitations.Select(inv => _mapper.Map<InvitationDetailDto>(inv));

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
