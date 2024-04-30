using auth_service.Models;

namespace auth_service.Services.IService;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> roles);
}