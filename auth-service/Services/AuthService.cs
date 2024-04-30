using auth_service.Data;
using auth_service.Models;
using auth_service.Models.Dto;
using auth_service.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace auth_service.Services;

public class AuthService : IAuthService
{

    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext db,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<string> Register(RegisterationRequestDto regiterationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = regiterationRequestDto.Email,
            Email = regiterationRequestDto.Email,
            NormalizedEmail = regiterationRequestDto.Email.ToUpper(),
            Name = regiterationRequestDto.Name,
            PhoneNumber = regiterationRequestDto.PhoneNumber
        };
        try
        {
            var result = await _userManager.CreateAsync(user, regiterationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == regiterationRequestDto.Email);
                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    ID = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };
                return "";

            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }

        }
        catch (Exception e)
        {
            return "error encounterd";
        }

    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
        if (user == null || isValid == false)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        UserDto userDto = new()
        {
            Email = user.UserName,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };
        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDto,
            Token = token
        };
        return loginResponseDto;
    }

    public async Task<bool> AssignedRole(string Email, string rolename)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == Email.ToLower());
        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult())
            {
                //create roll
                _roleManager.CreateAsync(new IdentityRole(rolename)).GetAwaiter().GetResult();

            }
            await _userManager.AddToRoleAsync(user, rolename);
            return true;
        }

        return false;
    }
}
