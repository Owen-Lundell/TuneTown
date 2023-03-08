using Microsoft.AspNetCore.Identity;

namespace TuneTown.Models
{
    public class UserVM
    {
        //= null! is a null forgiving operator that only supresses warnings
        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
