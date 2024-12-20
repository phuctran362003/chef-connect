using ChefConnect.Domain.Entities;

namespace ChefConnect.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User newUser);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string hashedPassword);
        Task<User> GetUserByEmailOrUsernameAsync(string emailOrUsername);
        Task<User> GetUserByEmailAsync(string email);


    }
}
