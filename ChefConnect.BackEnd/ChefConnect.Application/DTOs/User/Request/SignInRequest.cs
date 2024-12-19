namespace ChefConnect.Application.DTOs.User.Request
{
    // Request model for Sign-In
    public class SignInRequest
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}
