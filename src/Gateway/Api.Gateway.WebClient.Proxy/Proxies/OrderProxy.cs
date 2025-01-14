using Api.Gateway.Domain.Order.Command;
using Api.Gateway.Domain.Order.DTOs;
using Api.Gateway.WebClient.Proxy.Interfaces;
using Api.Gateway.WebClient.Proxy.Urls;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Common.Collection;

namespace Api.Gateway.WebClient.Proxy.Proxies
{
    public class OrderProxy(IOptions<ApiUrls> options,
        HttpClient client,
        IHttpContextAccessor httpContext,
        ICustomerProxy customerProxy,
        ICatalogProxy catalogProxy) : GenericProxy<OrderCreateCommand, OrderDto, int>(options, client, httpContext), IOrderProxy
    {
        private readonly ICustomerProxy _customerProxy = customerProxy;
        private readonly ICatalogProxy _catalogProxy = catalogProxy;


        protected override string GetBaseUrl()
        {
            return _urls.ApiGatewayUrl + "/Api/Orders";
        }
        public override async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int> selects = null)
        {
            var result = await base.GetAllAsync(page, take, selects);

            if (!result.HasItems)
            {
                return result;
            }

            var clientIds = result.Items!.Select(x => x.ClientId).Distinct(); // Distinct para evitar duplicados
            var clients = await _customerProxy.GetAllAsync(1, clientIds.Count(), clientIds);

            if (clients.HasItems)
            {
                foreach (var order in result.Items!)
                {
                    order.Client = clients.Items!.FirstOrDefault(x => x.Id == order.ClientId);
                }
            }

            return result;
        }

        public override async Task<OrderDto> GetAsync(int id)
        {
            var result = await base.GetAsync(id);
            result.Client = await _customerProxy.GetAsync(result.ClientId);


            //Avoid to use innecesary logic
            if (result.Items == null)
            {
                return result;
            }

            var productsIds = result.Items.Select(x => x.ProductId).Distinct().ToList();

            //retrieve products details
            var products = await _catalogProxy.GetAllAsync(1, productsIds.Count(), productsIds);


            //Asign Product per every item from order
            foreach (var orderDetail in result.Items!)
            {
                orderDetail.Product = products.Items!.FirstOrDefault(x => x.Id == orderDetail.ProductId);
            }

            return result;


        }
    }
}
