namespace Order.Services.EventHandler.Command
{
    public class OrderDetailCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
