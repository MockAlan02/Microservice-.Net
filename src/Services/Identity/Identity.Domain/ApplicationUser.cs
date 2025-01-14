using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<ApplicationRole> applicationRoles { get; set; } = [];
    }
}
