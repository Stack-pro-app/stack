using messaging_service.models.domain;
using messaging_service.models.dto.Response;
using messaging_service.models.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using messaging_service.Repository;
using AutoMapper;

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

        [HttpPost("{name}")]
        public async Task<ActionResult<ResponseDto>> CreateUser([FromRoute]string name)
        {
            try
            {
                bool result = await _repository.CreateWorkspaceAsync(name);
                if (result)
                {
                    ResponseDto response = new()
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Workspace Succesfully Created",
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
