using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.Configuration
{
    internal class ProductInStockConfiguration : IEntityTypeConfiguration<ProductInStock>
    {
        public void Configure(EntityTypeBuilder<ProductInStock> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
