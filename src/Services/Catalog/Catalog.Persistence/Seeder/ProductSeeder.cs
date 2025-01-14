using Catalog.Domain;

namespace Catalog.Persistence.Seeder
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Products.Any())
                return;


            //seed data default
            List<Product> products = [];
            var random = new Random();
            for (int i = 1; i < 100; i++)
            {
                products.Add(new()
                {
                    Name = $"Product{i}",
                    Description = $"Este producto es Product{i}",
                    Price = random.Next(100, 1000),
                });
            }

            await context.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
