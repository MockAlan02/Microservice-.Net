using Api.Gateway.Domain.Order.DTOs;
using Api.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Common.Collection;

namespace Clients.WebClient.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class IndexModel(ILogger<IndexModel> logger, IOrderProxy orderProxy) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;
        private readonly IOrderProxy _orderProxy = orderProxy;

        public DataCollection<OrderDto> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public async Task OnGet(int currentPage = 1)
        {
            CurrentPage = currentPage;
            await LoadAsync();
        }

        public int GetNextPage()
        {
            return CurrentPage + 1;
        }

        public int GetPreviousPage()
        {
            return CurrentPage - 1;
        }

        public async Task LoadAsync()
        {
            if (CurrentPage != 0 && CurrentPage != -1)
            {
                Orders = await _orderProxy.GetAllAsync(CurrentPage, 10);

                if (Orders != null)
                {
                    HasNextPage = CurrentPage < Orders.Pages;
                    HasPreviousPage = CurrentPage > 1;
                }
            }

        }
    }
}
