using ChefConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefConnect.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int ChefId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        [Column(TypeName = "nvarchar(50)")]

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public decimal TotalPrice { get; set; }
        public string Note { get; set; }

        public User User { get; set; }
        public ChefInformation Chef { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
