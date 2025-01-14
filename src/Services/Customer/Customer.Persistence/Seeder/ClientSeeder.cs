using Customer.Domain;
using Customer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Customer.Persistence.Seeder
{
    public static class ClientSeeder
    {
        public static async Task Seeder(ApplicationDbContext context)
        {
            if (await context.Clients.CountAsync() > 1)
                return;

            List<Client> clients = [
                new(){
                    Name = "Alan"
                },
                new(){
                    Name = "Pedro"
                }
                ];

            await context.AddRangeAsync(clients);
            await context.SaveChangesAsync();
        }
    }
}
