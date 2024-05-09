using Microsoft.AspNetCore.Mvc;
using messaging_service.Repository.Interfaces;
using AutoMapper;
using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Response;
using messaging_service.models.dto.Others;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.Filters;

namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceRepository _repository;
        private readonly IMapper _mapper;
        public WorkspaceController(IWorkspaceRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Used To Create a New Workspace by a User which will be the admin of the workspace
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateWorkspace([FromBody]WorkspaceRequestDto workspace)
        {
                Workspace ws = _mapper.Map<Workspace>(workspace);
                await _repository.CreateWorkspaceAsync(ws);
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Succesfully Created",
                    };
                    return Ok(response);
        }
        /// <summary>
        /// Used To delete a workspace
        /// </summary>
        [HttpDelete("{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> DeleteWorkspace([FromRoute]int workspaceId)
        {
                await _repository.DeleteWorkspaceAsync(workspaceId);
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Succesfully Deleted",
                    };
                    return Ok(response);

        }
        /// <summary>
        /// Used To Change the workspace's name
        /// </summary>
        [HttpPut("{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> ChangeWorkspaceName([FromRoute]int workspaceId,[FromQuery]WorkspaceName workspace)
        {

                await _repository.UpdateWorkspaceAsync(workspaceId, workspace.Name);
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Name Succesfully Updated",
                    };
                    return Ok(response);
        }
        /// <summary>
        /// Used When you enter a workspace to get the workspace's details including the channels (userId to get what the user has in the workspace)
        /// </summary>
        [HttpGet("{workspaceId}")]
        public async Task<ActionResult<ResponseDto>> GetWorkspace([FromRoute]int workspaceId, [FromQuery]int userId)
        {
            try
            {
                WorkspaceDetailDto workspace = await _repository.GetWorkspaceAsync(workspaceId,userId);
                ResponseDto response = new()
                {
                    Result = workspace,
                    IsSuccess = true,
                    Message = "Succesfully got your workspace!",
                };
                return Ok(response);
            }
            catch(Exception) {
                ResponseDto response = new()
                {
                    IsSuccess = false,
                    Message = "Failed to get your workspace!",
                };
                return BadRequest(response);
            }
        }


    }
}
