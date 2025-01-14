using System.Diagnostics.Contracts;

namespace Order.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        //Navigation Property
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
