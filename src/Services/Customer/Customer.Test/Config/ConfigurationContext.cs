

using Customer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Customer.Test.Config
{
    public static class ConfigurationContext
    {
        public static ApplicationDbContext GetContext()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Customer.Db")
                .Options;

            return new ApplicationDbContext(option);
        }
    }
}
