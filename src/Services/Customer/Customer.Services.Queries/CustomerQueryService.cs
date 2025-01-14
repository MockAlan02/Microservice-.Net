using Customer.Persistence.Context;
using Customer.Services.Queries.DTOs;
using Customer.Services.Queries.Interfaces;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Customer.Services.Queries
{
    public class CustomerQueryService(ApplicationDbContext context) : ICustomerQueryService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int>? clients = null)
        {
            var collection = await _context.Clients.Where(x => clients == null || clients.Contains(x.Id))
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ClientDto>>()!;
        }

        public async Task<ClientDto> GetAsync(int id)
        {
            return (await _context.Clients.FindAsync(id))!.MapTo<ClientDto>()!;
        }
    }
}
