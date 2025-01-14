using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using Order.Service.Proxies.Catalog.Interface;
using System.Text;
using System.Text.Json;

namespace Order.Service.Proxies.Catalog
{

    public class CatalogHttpProxy : ICatalogProxy
    {
        private readonly HttpClient _httpClient;

        private readonly ApiUrls _apiUrls;
        public CatalogHttpProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient, IHttpContextAccessor httpContext)
        {
            httpClient!.AddBearerToken(httpContext);
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }
        //httpClient.AddBearerToken(httpContext);


        public async Task UpdateStockAsync(ProductInStockUpdateCommand command)
        {
            //serialize object to json
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
                );
            var url = _apiUrls.CatalogUrl;
            //request httpClient to update stock in product
            var request = await _httpClient.PutAsync(_apiUrls.CatalogUrl, content);
            request.EnsureSuccessStatusCode();
        }
    }
}
