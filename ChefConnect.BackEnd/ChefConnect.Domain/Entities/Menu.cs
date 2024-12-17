using ChefConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefConnect.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public int ChefId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public MenuStatus Status { get; set; } = MenuStatus.Active;

        public ChefInformation Chef { get; set; }
    }
}
