using System.ComponentModel.DataAnnotations;

namespace Api.Gateway.Domain.Order.Command
{
    public class OrderDetailCommand
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}
