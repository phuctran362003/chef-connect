using ChefConnect.Domain.Enums;

namespace ChefConnect.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DishStatus Status { get; set; } = DishStatus.Available;

        public Menu Menu { get; set; }
    }
}
