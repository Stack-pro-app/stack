﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using auth_service.Models;
using auth_service.Services.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace auth_service.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value ;
    }
    
    public string GenerateToken(ApplicationUser applicationUser,IEnumerable<string> roles)
    {
        var tokenHolder = new JwtSecurityTokenHandler();
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        var key = Encoding.ASCII.GetBytes(jwtKey??"SECRET");
        var claimList = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
            new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName)


        };
        claimList.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));
            
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer,
            Subject = new ClaimsIdentity(claimList),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


        };
        var token = tokenHolder.CreateToken(tokenDescriptor);
        return tokenHolder.WriteToken(token);
    }
}