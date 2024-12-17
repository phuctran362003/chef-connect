using ChefConnect.Domain.Entities;

namespace ChefConnect.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user, string password);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsUsernameExistsAsync(string username);
    }
}
