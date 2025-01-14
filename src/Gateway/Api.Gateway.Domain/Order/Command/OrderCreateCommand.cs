using Api.Gateway.Domain.Order.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Api.Gateway.Domain.Order.Command
{
    public class OrderCreateCommand 
    {
        [Required]
        public OrderPayment PaymentType { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The ClientId must be greater than 0.")]
        public int ClientId { get; set; }
        public List<OrderDetailCommand>? Items { get; set; } = [];
    }
}
