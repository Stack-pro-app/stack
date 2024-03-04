using messaging_service.models.dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using messaging_service.models.domain;
using messaging_service.Repository;
using AutoMapper;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Requests;

namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly ChannelRepository _repository;
        private readonly IMapper _mapper;
        public ChannelController(ChannelRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //Create A Channel (Private or Public)
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateChannel([FromBody]ChannelRequestDto channelDto)
        {
            try
            {
                Channel channel = _mapper.Map<Channel>(channelDto);
                var result = await _repository.CreateChannelAsync(channel);
                if (!result) throw new Exception("Can't create Channel");
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Channel Successfully Created!",
                    Result = null,
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Create Channel!"+ex.Message,
                    Result = null,
                };
                return BadRequest(response);
            }

        }
        //Delete A Channel
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteChannel([FromRoute]int id)
        {
            try
            {
                bool result = await _repository.DeleteChannelAsync(id);
                if (!result) throw new Exception("Can't Delete Channel");
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Succesfully deleted Channel",
                };
                    
             return Ok(response);
            }
            catch(Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Create Channel!" + ex.Message,
                    Result = null,
                };
                return BadRequest(response);
            }
        }

        //Update Channel
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateChannel([FromBody]ChannelUpdateDto channelDto)
        {
            try
            {
                Channel channel = _mapper.Map<Channel>(channelDto);
                bool result = await _repository.UpdateChannelAsync(channel);
                if (!result) throw new Exception("Can't Update Channel");
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Succesfully updated the Channel!",
                    Result = null,
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Update Channel!" + ex.Message,
                    Result = null,
                };
                return BadRequest(response);
            }
        }

        //Get a channel info
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
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Find Channel!" + ex.Message,
                    Result = null,
                };
                return BadRequest(response);
            }
        }

        //Add Users To A Private Channel
        [HttpPost("AddUser/{channelId}")]
        public async Task<ActionResult<ResponseDto>> AddToPrivateChannel([FromRoute] int channelId,[FromBody] int userId)
        {
            try
            {
                bool result = await _repository.AddUserToPrivateChannel(channelId, userId);
                if (!result) throw new Exception("Can't Add User To Channel");
                ResponseDto responseDto = new()
                {
                    IsSuccess = true,
                    Message = "User Added To Channel Successfully",
                };
                return Ok(responseDto);

            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Add User To Channel!" + ex.Message,
                    Result = null,
                };
                return BadRequest(response);
            }
        }


    }
}
