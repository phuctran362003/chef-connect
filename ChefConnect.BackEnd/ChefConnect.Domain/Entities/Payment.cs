using ChefConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefConnect.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public string TransactionId { get; set; }

        public Order Order { get; set; }
    }
}
