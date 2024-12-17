namespace ChefConnect.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public int ChefId { get; set; }
        public string Status { get; set; } = "pending";
        public string PaymentStatus { get; set; } = "unpaid";
        public decimal TotalPrice { get; set; }
        public string Note { get; set; }

        public User User { get; set; }
        public ChefInformation Chef { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
