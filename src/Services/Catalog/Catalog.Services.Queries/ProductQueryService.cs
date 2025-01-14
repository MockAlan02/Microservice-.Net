using Catalog.Persistence;
using Catalog.Services.Queries.DTOs;
using Catalog.Services.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Catalog.Services.Queries
{
    public class ProductQueryService(ApplicationDbContext context) : IProductQueryService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<DataCollection<ProductDto>?> GetAllAsync(int page, int take, IEnumerable<int>? products = null)
        {
            var collection = await _context.Products.Where(x => products == null || products.Contains(x.Id))
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<ProductDto>>();
        }

        public async Task<ProductDto?> GetAsync(int id)
        {
            return (await _context.Products.SingleAsync(x => x.Id == id)).MapTo<ProductDto>();
        }
    }
}
