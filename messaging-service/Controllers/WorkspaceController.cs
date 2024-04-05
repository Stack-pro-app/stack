

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using messaging_service.Repository;
using AutoMapper;
using messaging_service.models.dto.Requests;
using messaging_service.models.dto.Response;
using messaging_service.models.dto.Others;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly WorkspaceRepository _repository;
        private readonly IMapper _mapper;
        public WorkspaceController(WorkspaceRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateWorkspace([FromBody]WorkspaceRequestDto workspace)
        {
                Console.Write(workspace);
                await _repository.CreateWorkspaceAsync(workspace.Name,workspace.userId);
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Succesfully Created",
                    };
                    return Ok(response);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteWorkspace([FromRoute]int id)
        {
                await _repository.DeleteWorkspaceAsync(id);
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Succesfully Deleted",
                    };
                    return Ok(response);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> ChangeWorkspaceName([FromRoute]int id,[FromBody]WorkspaceName workspace)
        {

                await _repository.UpdateWorkspaceAsync(id,workspace.Name);
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Name Succesfully Updated",
                    };
                    return Ok(response);
        }
        //Workspace and it's channels by Id (and the user's Id extacted from jwt Token!)
        [HttpGet("{id}/user/{userId}")]
        public async Task<ActionResult<ResponseDto>> GetWorkspace([FromRoute]int id, [FromRoute]int userId)
        {
            try
            {
                WorkspaceDetailDto workspace = await _repository.GetWorkspaceAsync(id,userId);
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
