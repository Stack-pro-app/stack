using messaging_service.models.dto;
using messaging_service.models.dto.Response;
using messaging_service.models.domain;
using messaging_service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;


namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(UserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateUser([FromBody]UserDto userDto)
        {

            /*IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new RS256Algorithm(certificate);
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

            var json = decoder(token);
            int generalId = json.Id;*/

            try
            {
                var user = _mapper.Map<User>(userDto);
                bool result = await _userRepository.CreateUserAsync(user);
                if (result)
                {
                    ResponseDto response = new()
                    {
                        IsSuccess = true,
                        Message = "Successfully Created !",
                    };
                    
                    return Ok(response);
                }
                else
                {
                    throw new Exception("Failled to add !");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{authId}")]
        public async Task<ActionResult<ResponseDto>> GetUser([FromRoute]int authId)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(authId);
                if (user == null) throw new Exception("No User Was Found");
                var userResponseDto = _mapper.Map<UserResponseDto>(user);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = userResponseDto,
                    Message = "Welcome !"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{authId}")]
        public async Task<ActionResult<ResponseDto>> DeleteUser([FromRoute]int authId)
        {
            try
            {
                bool result = await _userRepository.DeleteUserAsync(authId);
                if (!result) throw new Exception("Failed To Delete !");
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = null,
                    Message = "Successfully Deleted",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateUser([FromBody]UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                bool result = await _userRepository.UpdateUserAsync(user);
                if (!result) throw new Exception("can't find user");
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Custom Apis Here
        // Add Users To WorkSpace
        [HttpPost("Workspace")]
        public async Task<ActionResult<ResponseDto>> AddUsersToWorkspace([FromBody]UsersWorkSpaceDto usersDto)
        {
            try
            {
                IEnumerable<string> result = await _userRepository.AddUsersToWorkspace(usersDto.WorkspaceId, usersDto.UsersId);
                if (!result.Any()) throw new Exception("Can't Add Users To Workspace");
                ResponseDto response = new()
                {
                    Result = result.ToList(),
                    IsSuccess = true,
                    Message = " Added Users To Workspace",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message,
                };
                return BadRequest(response);
            }

        }
        // Get Users By WorkspaceId
        [HttpGet("Workspace/{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> GetUsersByWorkspaceId([FromRoute]int workspaceId)
        {
            try
            {
                var users = await _userRepository.GetUsersByWorkspaceAsync(workspaceId);
                if (users.IsNullOrEmpty()) throw new Exception("Can't find any users");
                ResponseDto response = new()
                {
                    Result = users,
                    IsSuccess = true,
                    Message = "Users from your workspace",
                };
                return Ok(users);
            }
            catch(Exception ex)
            {
                ResponseDto response = new()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message,
                };
                return BadRequest(response);
            }
        }
        [HttpDelete("Workspace")]
        public async Task<ActionResult<ResponseDto>> GetUsersByWorkspaceId([FromBody]UsersWorkSpaceDto usersDto)
        {
            try
            {
                IEnumerable<string> result = await _userRepository.RemoveUserFromWorkspace(usersDto.WorkspaceId, usersDto.UsersId);
                if (!result.Any()) throw new Exception("Can't Add Users To Workspace");
                ResponseDto response = new()
                {
                    Result = result.ToList(),
                    IsSuccess = true,
                    Message = " Deleted Users From Workspace",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                ResponseDto response = new()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message,
                };
                return BadRequest(response);
            }
        }


    }
}
