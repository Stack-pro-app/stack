using messaging_service.models.dto.Response;
using messaging_service.models.domain;
using messaging_service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Requests;


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


        //Create a new Chat User
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateUser([FromBody]UserMinimalDto userDto)
        {
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


        // Get User by Authentification Id
        [HttpGet("{authId}")]
        public async Task<ActionResult<ResponseDto>> GetUser([FromRoute]string authId)
        {
            try
            {
                var user = await _userRepository.GetUserAsync(authId);
                if (user == null) throw new Exception("No User Was Found");
                var userResponseDto = _mapper.Map<UserDetailDto>(user);
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


        //Delete User By authentification Id
        [HttpDelete("{authId}")]
        public async Task<ActionResult<ResponseDto>> DeleteUser([FromRoute]string authId)
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


        //Update User's Name & Email (and authId if needed)
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateUser([FromBody]UserMinimalDto userDto)
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


        //***************************** Custom Apis Here *************************************************************************


        // Add multiple Users To a WorkSpace
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


        // Get Users In a Workspace by workspaceId
        [HttpGet("Workspace/{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> GetUsersByWorkspaceId([FromRoute]int workspaceId)
        {
            try
            {
                IEnumerable<UserDetailDto> users = await _userRepository.GetUsersByWorkspaceAsync(workspaceId);
                if (users.IsNullOrEmpty()) throw new Exception("Can't find any users");
                ResponseDto response = new()
                {
                    Result = users.ToList(),
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
        // Get Users In a Channel by channelId
        [HttpGet("channel/{channelId}")]
        public async Task<ActionResult<ResponseDto>> GetUsersByChannelId([FromRoute] int channelId)
        {
            try
            {
                IEnumerable<User> users = await _userRepository.GetUsersByChannelAsync(channelId);
                IEnumerable<UserDetailDto> usersDto = users.Select(user => _mapper.Map<UserDetailDto>(user));
                Console.WriteLine(usersDto);
                if (users.IsNullOrEmpty()) throw new Exception("Can't find any users");
                ResponseDto response = new()
                {
                    Result = usersDto,
                    IsSuccess = true,
                    Message = "Users from your channel",
                };
                return Ok(users);
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


        //  Multiple Users From a workspace by workspaceId & UsersIds
        [HttpDelete("Workspace")]
        public async Task<ActionResult<ResponseDto>> RemoveUsersFromWorkspace([FromBody]UsersWorkSpaceDto usersDto)
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


        // Get Your User by Email
        [HttpGet("email/{email}")]
        public async Task<ActionResult<ResponseDto>> GetUserByEmail([FromRoute]string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null) throw new Exception("No User Was Found");
                var userResponseDto = _mapper.Map<UserDetailDto>(user);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = userResponseDto,
                    Message = "User Found!"
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


        /// <summary>
        /// This Api Sets the Login time for a user & sends back his workspaces
        /// </summary>
        [HttpGet("myworkspaces/{authId}")]
        public async Task<ActionResult<ResponseDto>> LoginAndGetUserWorkspaces([FromRoute]string authId)
        {
            try
            {
                User user = await _userRepository.GetUserAsync(authId);
                if (user == null) throw new Exception("No User Was Found");
                UserDetailDto userResponseDto = _mapper.Map<UserDetailDto>(user);
                IEnumerable<Workspace> workspacs = await _userRepository.SetLoginAndGetWorkspaces(authId) ?? throw new Exception("Can't find user or workpaces");
               // IEnumerable<WorkspaceMinimalDto> workspacesDto = _mapper.Map<IEnumerable<WorkspaceMinimalDto>>(workspaces);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = new
                    {
                        user = userResponseDto,
                        workspaces = workspacs
                    },
                    Message = "Successfully logged in to the Chat!"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
