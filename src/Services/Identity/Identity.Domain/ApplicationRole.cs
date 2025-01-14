using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = [];
    }
}
