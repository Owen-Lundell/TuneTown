using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuneTown.Models
{
    public class AppUser : IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }

        public DateTime SignUpDate { get; set; }
    }
}
