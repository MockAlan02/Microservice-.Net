using Api.Gateway.Domain.Identity.Commands;
using Api.Gateway.Domain.Identity.Responses;

namespace Api.Gateway.Proxies.Interfaces
{
    public interface IIdentityProxy
    {
        Task<IdentityAccess> Authentication(UserLoginCommand command);
        Task CreateAsync(UserCreateCommand command);
    }
}