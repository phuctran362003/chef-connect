using ChefConnect.Domain.Enums;

namespace ChefConnect.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public int ChefId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MenuStatus Status { get; set; } = MenuStatus.Active;

        public ChefInformation Chef { get; set; }
    }
}
