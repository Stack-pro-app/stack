﻿namespace auth_service.Models.Dto;

public class RegisterationRequestDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    //public string? PhoneNumber { get; set; } Useless
    public string Password { get; set; }
    public string? Rolename { get; set; }

}