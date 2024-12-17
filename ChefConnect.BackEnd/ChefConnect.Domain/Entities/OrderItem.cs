namespace ChefConnect.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }

        public Order Order { get; set; }
        public Dish Dish { get; set; }
    }

}
