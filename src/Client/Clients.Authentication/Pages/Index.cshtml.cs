using Api.Gateway.Domain.Identity.Responses;
using Clients.Authentication.Urls;
using Clients.Authentication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
using Clients.Authentication.Helper;
using Clients.Authentication.ApiResponse;
using Service.Common.ApiResponse.ApiResponse;

namespace Clients.Authentication.Pages
{
    public class IndexModel(ILogger<IndexModel> logger, IOptions<Url> options) : PageModel
    {
        public ApiErrorResponse ErrorResponse { get; set; }
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly HttpClient _httpClient = new();
        private readonly Url _url = options.Value;

        public object JsonConvert { get; private set; }

        public void OnGet()
        {

        }

        [BindProperty]
        public LoginViewModel? Vm { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var content = new StringContent(
                JsonSerializer.Serialize(Vm),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_url.IdentityUrl + "/Authentication", content);
            await response!.AddErrorsToModelState(ModelState);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = JsonSerializer.Deserialize<ApiResponse<IdentityAccess>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
            return Redirect($"{_url.WebClientUrl}/Account/Connect?token={result!.Data!.AccessToken}");
        }


    }
}
