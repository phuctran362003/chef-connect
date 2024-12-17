namespace ChefConnect.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; } = "pending";
        public string TransactionId { get; set; }

        public Order Order { get; set; }
    }
}
