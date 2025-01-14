using MediatR;
using Order.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Order.Services.EventHandler.Command
{
    public class OrderCreateCommand : INotification
    {
        [Required]
        public OrderPayment PaymentType { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The ClientId must be greater than 0.")]
        public int ClientId { get; set; }
        public List<OrderDetailCommand> Items { get; set; } = [];
    }
}
