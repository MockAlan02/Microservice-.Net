using Catalog.Services.Queries.DTOs;
using Service.Common.Collection;

namespace Catalog.Services.Queries.Interfaces
{
    public interface IProductQueryService
    {
        Task<DataCollection<ProductDto>?> GetAllAsync(int page, int take, IEnumerable<int>? products = null);
        Task<ProductDto?> GetAsync(int id);
    }
}