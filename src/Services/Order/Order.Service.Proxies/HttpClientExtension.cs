using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Order.Service.Proxies
{
    public static class HttpClientExtension
    {
        public static void AddBearerToken(this HttpClient client, IHttpContextAccessor context)
        {
            if (context.HttpContext.User.Identity!.IsAuthenticated && context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ");
                if (token.Length > 0)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token[1]);
                }
            }
        }
    }
}
