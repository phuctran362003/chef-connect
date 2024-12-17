namespace ChefConnect.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public int ChefId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "active";

        public ChefInformation Chef { get; set; }
    }
}
