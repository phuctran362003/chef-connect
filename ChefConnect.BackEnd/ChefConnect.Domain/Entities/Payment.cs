using ChefConnect.Domain.Enums;

namespace ChefConnect.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public string TransactionId { get; set; }

        public Order Order { get; set; }
    }
}
