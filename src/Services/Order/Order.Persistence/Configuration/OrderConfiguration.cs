using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Persistence.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Domain.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Items).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
