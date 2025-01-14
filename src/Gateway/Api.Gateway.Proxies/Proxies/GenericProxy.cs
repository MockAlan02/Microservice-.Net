using Api.Gateway.Proxies.Extension;
using Api.Gateway.Proxies.Interfaces;
using Api.Gateway.Proxies.Urls;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Service.Common.Collection;
using System.Text;
namespace Api.Gateway.Proxies.Proxies
{
    /// <summary>
    /// Provides a generic proxy for managing API calls for entities of type <typeparamref name="TDto"/> 
    /// with commands of type <typeparamref name="TCommand"/>.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command object used for creating entities.</typeparam>
    /// <typeparam name="TDto">The type of the data transfer object representing entities.</typeparam>
    /// <typeparam name="TKey">The type of the key used to identify entities.</typeparam>
    public abstract class GenericProxy<TCommand, TDto, TKey> : IGenericProxy<TCommand, TDto, TKey>
        where TCommand : class
        where TDto : class
    {
        /// <summary>
        /// API URL configurations.
        /// </summary>
        private protected readonly ApiUrls _urls;

        /// <summary>
        /// HTTP client for making API requests.
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericProxy{TCommand, TDto, TKey}"/> class.
        /// </summary>
        /// <param name="options">API URL configurations.</param>
        /// <param name="client">The HTTP client instance.</param>
        /// <param name="httpContext">The HTTP context accessor for retrieving tokens.</param>
        public GenericProxy(IOptions<ApiUrls> options, HttpClient client, IHttpContextAccessor httpContext)
        {
            client.AddBearerToken(httpContext);
            _urls = options.Value;
            _client = client;

        }

        /// <summary>
        /// Gets the base URL for API requests.
        /// </summary>
        /// <returns>The base URL as a string.</returns>
        protected abstract string GetBaseUrl();

        /// <inheritdoc/>
        public async Task CreateAsync(TCommand command)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(command),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(GetBaseUrl(), content);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data from {GetBaseUrl()}: {response.ReasonPhrase}");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<DataCollection<TDto>> GetAllAsync(int page, int take, IEnumerable<int>? selects = null)
        {
            var url = $"{GetBaseUrl()}?page={page}&take={take}";
            if (selects != null)
            {
                var selectsQuery = string.Join(",", selects);
                url += $"&ids={selectsQuery}";
            }
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data from {url}: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DataCollection<TDto>>(content)!;
        }

        /// <inheritdoc/>
        public virtual async Task<TDto> GetAsync(TKey id)
        {
            var url = $"{GetBaseUrl()}/{id}";
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data from {url}: {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TDto>(content)!;
        }
    }
}
