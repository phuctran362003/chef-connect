using ChefConnect.Domain.Entities;

namespace ChefConnect.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsUsernameExistsAsync(string username);
    }
}
