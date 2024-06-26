﻿using messaging_service.models.dto.Response;
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
using System.ComponentModel.DataAnnotations;
using messaging_service.Repository.Interfaces;
using messaging_service.Filters;
using messaging_service.Models.Dto.Requests;
using Amazon.S3.Model;
using Amazon.S3;


namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAmazonS3 _s3Client;
        private int MaxFileSize = 50 * 1024 * 1024;
        public UserController(IUserRepository userRepository,IMapper mapper,IAmazonS3 s3)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _s3Client = s3;
        }


        /// <summary>
        /// Creates A new messaging service user.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateUser([FromBody]UserMinimalDto userDto)
        {
                var user = _mapper.Map<User>(userDto);
                await _userRepository.CreateUserAsync(user);
                    ResponseDto response = new()
                    {
                        IsSuccess = true,
                        Message = "Successfully Created !",
                    };
                    
                    return Ok(response);

        }


        /// <summary>
        /// Get User Info using Authentication Id (required to use in order to get the messaging service user Id)
        /// </summary>
        [HttpGet("{authId}")]
        public async Task<ActionResult<ResponseDto>> GetUser([FromRoute]string authId)
        {
                var user = await _userRepository.GetUserAsync(authId);
                var userResponseDto = _mapper.Map<UserDetailDto>(user);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = userResponseDto,
                    Message = "Welcome !"
                };
                return Ok(response);
        }
        /// <summary>
        /// Get User Info using Messaging Service Id
        /// </summary>
        [HttpGet("byId/{id}")]
        public async Task<ActionResult<ResponseDto>> GetUserById([FromRoute] int id)
        {
                var user = await _userRepository.GetUserAsync(id);
                var userResponseDto = _mapper.Map<UserDetailDto>(user);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = userResponseDto,
                    Message = "Welcome !"
                };
                return Ok(response);
        }


        /// <summary>
        /// Deletes User By Authentication Id
        /// </summary>
        [HttpDelete("{authId}")]
        public async Task<ActionResult<ResponseDto>> DeleteUser([FromRoute]string authId)
        {
                await _userRepository.DeleteUserAsync(authId);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = null,
                    Message = "Successfully Deleted",
                };
                return Ok(response);
        }

        /// <summary>
        /// Update User's Name and Email (authentication Id is needed)
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateUser([FromBody]UserMinimalDto userDto)
        {
                var user = _mapper.Map<User>(userDto);
                await _userRepository.UpdateUserAsync(user);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                };
                return Ok(response);
        }


        //***************************** Custom Apis Here *************************************************************************

        /// <summary>
        /// Add multiple Users To a WorkSpace (deprecated due to invite feature)
        /// </summary>
        [HttpPost("Workspace")]
        public async Task<ActionResult<ResponseDto>> AddUsersToWorkspace([FromBody]UsersWorkSpaceDto usersDto)
        {
                IEnumerable<string> result = await _userRepository.AddUsersToWorkspace(usersDto.WorkspaceId, usersDto.UsersId);
                ResponseDto response = new()
                {
                    Result = result.ToList(),
                    IsSuccess = true,
                    Message = "Added Users To Workspace",
                };
                return Ok(response);
        }

        /// <summary>
        /// Get User's info for a workspace Members by workspaceId
        /// </summary>
        [HttpGet("Workspace/{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> GetUsersByWorkspaceId([FromRoute]int workspaceId)
        {
                IEnumerable<UserDetailDto> users = await _userRepository.GetUsersByWorkspaceAsync(workspaceId);
                ResponseDto response = new()
                {
                    Result = users.ToList(),
                    IsSuccess = true,
                    Message = "Users from your workspace",
                };
                return Ok(response);
        }
        /// <summary>
        /// Get User's info for a channel Members by channelId (used for private channels especially)
        /// </summary>
        [HttpGet("channel/{channelId}")]
        public async Task<ActionResult<ResponseDto>> GetUsersByChannelId([FromRoute] int channelId)
        {
                IEnumerable<User> users = await _userRepository.GetUsersByChannelAsync(channelId);
                if (users == null) throw new ValidationException("Can't find any users");
                IEnumerable<UserDetailDto> usersDto = users.Select(user => _mapper.Map<UserDetailDto>(user));
                ResponseDto response = new()
                {
                    Result = usersDto,
                    IsSuccess = true,
                    Message = "Users from your channel",
                };
                return Ok(response);
        }

        /// <summary>
        /// Remove A User from a workspace using their Ids
        /// </summary>
        [HttpDelete("{id}/Workspace/{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> RemoveUsersFromWorkspace([FromRoute]int id, [FromRoute] int workspaceId)
        {
                await _userRepository.RemoveUserFromWorkspace(workspaceId, id);
                ResponseDto response = new()
                {
                    Result = null,
                    IsSuccess = true,
                    Message = "Successfully Deleted User From Workspace",
                };
                return Ok(response);
        }


        /// <summary>
        /// Get User's info by email (used to search for user before inviting)
        /// </summary>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<ResponseDto>> GetUserByEmail([FromRoute]string email)
        {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null) throw new ValidationException("No User Was Found");
                var userResponseDto = _mapper.Map<UserDetailDto>(user);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = userResponseDto,
                    Message = "User Found!"
                };
                return Ok(response);
        }


        /// <summary>
        /// Used to get the user's workspaces after logging (required to use!)
        /// </summary>
        [HttpGet("myworkspaces/{authId}")]
        public async Task<ActionResult<ResponseDto>> LoginAndGetUserWorkspaces([FromRoute]string authId)
        {
                User user = await _userRepository.GetUserAsync(authId);
                if (user == null) throw new ValidationException("No User Was Found");
                UserDetailDto userResponseDto = _mapper.Map<UserDetailDto>(user);
                IEnumerable<Workspace> workspaces = await _userRepository.SetLoginAndGetWorkspaces(authId) ?? throw new ValidationException("Can't find user or workpaces");
               IEnumerable<WorkspaceMinimalDto> workspacesDto = _mapper.Map<IEnumerable<WorkspaceMinimalDto>>(workspaces);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = new
                    {
                        user = userResponseDto,
                        workspaces = workspacesDto
                    },
                    Message = "Successfully logged in to the Chat!"
                };
                return Ok(response);
        }
        [HttpPut("Picture")]
        public async Task<ActionResult<ResponseDto>> SetProfilePicture([FromForm] ProfilePictureDto pictureDto)
        {
            try
            {
                if (pictureDto.picture == null) return BadRequest("No Picture Found");
                if (pictureDto.picture.ContentType != "image/jpeg" && pictureDto.picture.ContentType != "image/png") return BadRequest("Only JPEG and PNG files are allowed");
                if (pictureDto.authId.IsNullOrEmpty()) return BadRequest("No AuthId Found");
                if (pictureDto.picture.Length > MaxFileSize) return BadRequest("File size is too large");
                var bucketName = "stack-messaging-service";
                var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
                if (!bucketExists) return NotFound($"Bucket not found");
                var filePath = $"profile-picture/{pictureDto.picture.FileName}";
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = filePath,
                    InputStream = pictureDto.picture.OpenReadStream(),
                };
                request.Metadata.Add("Content-Type", pictureDto.picture.ContentType);
                await _s3Client.PutObjectAsync(request);
                var urlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = filePath,
                    Expires = DateTime.UtcNow.AddYears(1) // Set the expiration time to a far-future date
                };
                string url = _s3Client.GetPreSignedURL(urlRequest);
                // Store the Profile Picture
                await _userRepository.StoreProfilePicture(pictureDto.authId, url , filePath);
                ResponseDto response = new()
                {
                    IsSuccess = true,
                    Result = url,
                    Message = "Successfully Uploaded Profile Picture",
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
