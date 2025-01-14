using Api.Gateway.Domain.Catalog.DTOs;
using Api.Gateway.Domain.Customer.DTOs;
using Api.Gateway.Domain.Order.Command;
using Api.Gateway.Domain.Order.DTOs;
using Api.Gateway.WebClient.Proxy.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Common.Collection;


namespace Clients.WebClient.Pages.Order
{
    public class CreateModel(ICustomerProxy clientProxy,
        ICatalogProxy catalogProxy,
        IOrderProxy orderProxy) : PageModel
    {
        private readonly ICustomerProxy _clientProxy = clientProxy;
        private readonly ICatalogProxy _catalogProxy = catalogProxy;
        private readonly IOrderProxy _orderProxy = orderProxy;
        public DataCollection<ClientDto> ClientDto { get; set; } = new();
        public DataCollection<ProductDto> ProductDto { get; set; } = new();

        public async Task OnGet()
        {
            ClientDto = await _clientProxy.GetAllAsync(1, 100);
            ProductDto = await _catalogProxy.GetAllAsync(1, 100);
        }

        [BindProperty]
        public OrderCreateCommand? OrderCreateCommand { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ClientDto = await _clientProxy.GetAllAsync(1, 100);
                ProductDto = await _catalogProxy.GetAllAsync(1, 100);
                return Page();
            }

            if (OrderCreateCommand?.Items == null || OrderCreateCommand.Items.Count == 0)
            {
                ModelState.AddModelError(nameof(OrderCreateCommand.Items), "No items were added to the order.");
                ClientDto = await _clientProxy.GetAllAsync(1, 100);
                ProductDto = await _catalogProxy.GetAllAsync(1, 100);
                return Page();
            }

            await _orderProxy.CreateAsync(OrderCreateCommand!);
            return Redirect("/");
        }
    }
}
