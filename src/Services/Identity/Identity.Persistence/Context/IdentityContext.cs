using Identity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Context
{
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        }
    }
}
