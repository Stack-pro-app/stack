using messaging_service.models.dto.Response;
using Microsoft.AspNetCore.Mvc;
using messaging_service.models.domain;
using AutoMapper;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Others;
using messaging_service.Repository.Interfaces;

namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelRepository _repository;
        private readonly IMapper _mapper;
        public ChannelController(IChannelRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Creates a channel, public or private
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateChannel([FromBody]ChannelRequestDto channelDto)
        {
            try
            {
                Channel channel = _mapper.Map<Channel>(channelDto);
                await _repository.CreateChannelAsync(channel);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Channel Successfully Created!",
                    Result = null,
                };
                return Ok(response);
            }
            catch(Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Create Channel!",
                    Result = null,
                };
                return BadRequest(response);
            }

        }
        /// <summary>
        /// Deletes a channel by it's id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteChannel([FromRoute]int id)
        {
            try
            {
                await _repository.DeleteChannelAsync(id);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Succesfully deleted Channel",
                };
                    
             return Ok(response);
            }
            catch(Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Delete Channel!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Updates a channel (could be used to make it public)
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateChannel([FromBody]ChannelUpdateDto channelDto)
        {
            try
            {
                Channel channel = _mapper.Map<Channel>(channelDto);
                await _repository.UpdateChannelAsync(channel);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Succesfully updated the Channel!",
                    Result = null,
                };
                return Ok(response);
            }
            catch(Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Update Channel!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Used To get a channel by it's id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetChannel([FromRoute]int id)
        {
            try
            {
                ChannelDetailDto channel = await _repository.GetChannelAsync(id) ?? throw new Exception("Invalid ChannelId");
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Found it!",
                    Result = channel,
                };
                return Ok(response);

            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Find Channel!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Used To Add a user to a private channel
        /// </summary>
        [HttpPost("AddUser/{channelId}")]
        public async Task<ActionResult<ResponseDto>> AddToPrivateChannel([FromRoute] int channelId,[FromBody]UserMinDto user)
        {
            try
            {
                await _repository.AddUserToPrivateChannel(channelId, user.userId);
                ResponseDto responseDto = new()
                {
                    IsSuccess = true,
                    Message = "User Added To Channel Successfully",
                };
                return Ok(responseDto);

            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Add User To Channel!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Used To Remove a user from a private channel (first Id is the channel's id, the second is the user Id)
        /// </summary>
        [HttpDelete("{channelId}/RemoveUser/{id}")]
        public async Task<ActionResult<ResponseDto>> RemoveFromPrivateChannel([FromRoute] int channelId, [FromRoute]int id)
        {
            try
            {
                await _repository.RemoveUserFromPrivateChannel(channelId, id);
                ResponseDto responseDto = new()
                {
                    IsSuccess = true,
                    Message = "User Removed From Channel Successfully",
                };
                return Ok(responseDto);

            }
            catch (Exception)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Remove User From Channel!",
                    Result = null,
                };
                return BadRequest(response);
            }
        }
        /// <summary>
        /// Used To Get a One To One Channel info, it creates a new one if it's not already created
        /// </summary>
        [HttpPost("OneToOne")]
        public async Task<ActionResult<ResponseDto>> FindOrCreateOneToOneChannel([FromBody] OneToOneChannelRequest request)
        {
            var result = await _repository.GetOneToOneChannel(request);
            if (result == null)
            {
                result = await _repository.CreateOneToOneChannel(request);
            }
            ChannelDetailDto channelDetailDto = _mapper.Map<ChannelDetailDto>(result);
            ResponseDto responseDto = new()
            {
                IsSuccess = true,
                Message = "Here's the One To One channel",
                Result = channelDetailDto
            };
            return Ok(responseDto);
        }
    }
}
