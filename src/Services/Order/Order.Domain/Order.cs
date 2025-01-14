using Order.Common.Enum;

namespace Order.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }

        public DateTime CreateAt { get; set; }
        public decimal Total { get; set; }

        //Navigation Property
        public ICollection<OrderDetail> Items { get; set; } = [];
    }
}
