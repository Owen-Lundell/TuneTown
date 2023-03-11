﻿using Microsoft.AspNetCore.Identity;

namespace TuneTown.Models
{
    public class UserVM
    {
        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
