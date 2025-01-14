using Catalog.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Test.Config
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Catalog.Db")
                .Options;
            //return Context Database
            return new ApplicationDbContext(option);
        }
    }
}
