using Order.Services.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Services.Queries.Interfaces
{
    public interface IOrderQueryService
    {
        Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int>? orders = null);
        Task<OrderDto> GetAsync(int id);
    }
}