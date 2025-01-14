using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Persistence.Context;
using Order.Services.Queries.DTOs;
using Order.Services.Queries.Interfaces;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Order.Services.Queries
{
    public class OrderQueryService(ApplicationDbContext context, IMapper mapper) : IOrderQueryService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int>? orders = null)
        {
            var result = await _context.Orders.Where(x => orders == null || orders.Contains(x.Id))
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(page, take);

            return result.MapTo<DataCollection<OrderDto>>()!;
        }


        public async Task<OrderDto> GetAsync(int id)
        {
            var result = (await _context.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id));
            return _mapper.Map<OrderDto>(result);
        }


    }
}
