using ChefConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefConnect.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public DishStatus Status { get; set; } = DishStatus.Available;

        public Menu Menu { get; set; }
    }
}
