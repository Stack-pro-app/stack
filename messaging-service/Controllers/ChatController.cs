using messaging_service.models.dto.Response;
using messaging_service.models.dto.Requests;
using messaging_service.models.domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using messaging_service.Repository;
using messaging_service.Consumer;
using AutoMapper;
using messaging_service.models.dto.Detailed;
using System.Threading.Channels;
using RabbitMQ.Client.Events;

namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatController(ChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> StoreMessage([FromBody]MessageRequestDto messageRequestDto)
        {
            try
            {
                Chat message = _mapper.Map<Chat>(messageRequestDto);
                bool result = await _chatRepository.CreateChatAsync(message);
                    ResponseDto response = new()
                    {
                        IsSuccess = true,
                        Message = "Succesfully Stored Your Message!"
                    };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Store Your Message!"
                };
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{messageId}")]
        public async Task<ActionResult<ResponseDto>> DeleteMessage([FromRoute]int messageId)
        {
            try
            {
                bool result = await _chatRepository.DeleteChatPartAsync(messageId);
                //bool result = await _chatRepository.DeleteChatPermAsync(messageId); // If You want to delete Permenantly!
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Succesfully Deleted Your Message!"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To Store Your Message!"
                };
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult<ResponseDto>> UpdateMessage([FromBody]MessageUpdateDto message)
        {
            try
            {
                bool result = await _chatRepository.UpdateChatAsync(message.Id, message.Message);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Succesfully Updated Your Message!"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed To update Your Message!"
                };
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetMessageById([FromRoute]int id)
        {
            try
            {
                MessageDetailDto result = await _chatRepository.GetMessageAsync(id);
                ResponseDto response = new()
                {
                    Result = result,
                    IsSuccess = true,
                    Message = "Here's The Message!"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Can't find the Message!"+ex.Message
                };
                return BadRequest(response);
            }
        }
        [HttpGet("channel/{channelId}")]
        public async Task<ActionResult<ResponseDto>> GetMessageByChannelId([FromRoute] int channelId, [FromQuery]int page)
        {
            try
            {
                IEnumerable<MessageDetailDto> messages = await _chatRepository.GetChannelLastMessagesAsync(channelId, page);
                ResponseDto response = new()
                {
                    Result = messages.ToList(),
                    IsSuccess = true,
                    Message = "Here's The Messages!"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Can't find the Messages!" + ex.Message
                };
                return BadRequest(response);
            }
        }


    }
}
