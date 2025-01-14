using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Api.Gateway.WebClient.Proxy.Extension
{
    public static class HttpClientExtension
    {
        public static void AddBearerToken(this HttpClient client, IHttpContextAccessor context)
        {
            if (context.HttpContext.User.Identity!.IsAuthenticated)
            {
                var token = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("access_token"))?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
        }
    }
}
