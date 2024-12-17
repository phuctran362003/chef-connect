namespace ChefConnect.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; } = "active";

        public Role Role { get; set; }
    }
}
