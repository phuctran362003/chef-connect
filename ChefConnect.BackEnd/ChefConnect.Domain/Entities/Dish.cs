namespace ChefConnect.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; } = "available";

        public Menu Menu { get; set; }
    }
}
