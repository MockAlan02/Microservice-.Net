using Api.Gateway.Domain.Catalog.DTOs;
using Api.Gateway.Domain.Catalog.Entities;

namespace Api.Gateway.Domain.Order.DTOs
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
