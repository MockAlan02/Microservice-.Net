using Api.Gateway.Domain.Order.DTOs;
using Api.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clients.WebClient.Pages.Order
{

    public class IndexModel(IOrderProxy orderProxy) : PageModel
    {
        private readonly IOrderProxy _orderProxy = orderProxy;
        public OrderDto? OrderDto { get; set; }
        public async Task OnGet(int? id)
        {
            if(id != 0)
            {
                OrderDto = await _orderProxy.GetAsync((int)id!);
            }
        }
    }
}
