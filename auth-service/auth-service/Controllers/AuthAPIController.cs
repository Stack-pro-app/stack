using auth_service.Models.Dto;
using auth_service.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace auth_service.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthAPIController : ControllerBase
{
    private readonly IAuthService _authService;
    protected ResponseDto _response;

    public AuthAPIController(IAuthService authService)
    {
        _authService = authService;
        _response = new();
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
    {
        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            _response.IsSuccess = false;
            _response.Message = errorMessage;
            return BadRequest(_response);
        }
        return Ok(_response);
    }
        
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {

        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "USERNAME OR PASSWORD INCORECT";
            return BadRequest(_response);
        }

        _response.Result = loginResponse;
        return Ok(_response);
    }
    [HttpPost("AssignRole")]
    public async Task<IActionResult> Login([FromBody] RegisterationRequestDto model)
    {

        var assignedRole = await _authService.AssignedRole(model.Email,model.Rolename.ToUpper());
        if (assignedRole)
        {
            _response.IsSuccess = false;
            _response.Message = "Error Encounterd";
            return BadRequest(_response);
        }

        _response.Result = assignedRole;
        return Ok(_response);
    }
}

