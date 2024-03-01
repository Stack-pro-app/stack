using auth_service.Models.Dto;

namespace auth_service.Services.IService;

public interface IAuthService
{
    Task<string> Register(RegisterationRequestDto regiterationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignedRole(string Email, string rolename);
}