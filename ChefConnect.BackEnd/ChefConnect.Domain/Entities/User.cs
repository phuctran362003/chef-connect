using ChefConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefConnect.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public int RoleId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public UserStatus Status { get; set; } = UserStatus.Active;

        public Role Role { get; set; }
    }
}
