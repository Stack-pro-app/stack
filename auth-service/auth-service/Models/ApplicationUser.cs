﻿using Microsoft.AspNetCore.Identity;

namespace auth_service.Models;

public class ApplicationUser: IdentityUser
{
    public string Name { get; set; }
}