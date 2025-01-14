using Customer.Services.Queries.DTOs;
using Service.Common.Collection;

namespace Customer.Services.Queries.Interfaces
{
    public interface ICustomerQueryService
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int>? clients = null);
        Task<ClientDto> GetAsync(int id);
    }
}