using Catalog.Domain;

namespace Catalog.Persistence.Seeder
{
    public static class ProductInStockSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.ProductInStocks.Any())
                return;


            List<ProductInStock> productInStocks = [];
            var random = new Random();
            for (int i = 1; i < 100; i++)
            {
                productInStocks.Add(new()
                {
                    ProductId = i,
                    Stock = random.Next(10, 1000)
                });
            }
            await context.AddRangeAsync(productInStocks);
            await context.SaveChangesAsync();
        }
    }
}
