using Api.Gateway.Domain.Identity.Commands;
using Api.Gateway.Domain.Identity.Responses;
using Api.Gateway.Proxies.Extension;
using Api.Gateway.Proxies.Interfaces;
using Api.Gateway.Proxies.Urls;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Api.Gateway.Proxies.Proxies
{

    public class IdentityProxy : IIdentityProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _client;
        public IdentityProxy(IOptions<ApiUrls> options, HttpClient client, IHttpContextAccessor httpContext)
        {
            client.AddBearerToken(httpContext);
            _urls = options.Value;
            _client = client;
        }
        public async Task<IdentityAccess> Authentication(UserLoginCommand command)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(command),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(_urls.IdentityUrl + "/Authentication", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data from {_urls.IdentityUrl}: {response.ReasonPhrase}");
            }

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IdentityAccess>(result)!;
        }


        public async Task CreateAsync(UserCreateCommand command)
        {
            var content = new StringContent(
             JsonConvert.SerializeObject(command),
             Encoding.UTF8,
             "application/json");

            var response = await _client.PostAsync(_urls.IdentityUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data from {_urls.IdentityUrl}: {response.ReasonPhrase}");
            }
        }
    }
}
