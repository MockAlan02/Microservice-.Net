using Catalog.Domain;
using Catalog.Persistence;
using Catalog.Service.EventHandlers.Commands;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Service.EventHandlers
{
    public class ProductCreateEventHandler(ApplicationDbContext context) : INotificationHandler<ProductCreateCommand>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

       
    }
}



