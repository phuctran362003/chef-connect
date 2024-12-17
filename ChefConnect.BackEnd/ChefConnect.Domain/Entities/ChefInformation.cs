namespace ChefConnect.Domain.Entities
{
    public class ChefInformation : BaseEntity
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public float Rating { get; set; }

        public User User { get; set; }
    }
}
