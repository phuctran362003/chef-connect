using ChefConnect.Domain.Entities;
using ChefConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChefConnect.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ChefConnectDbContext _context;

        public UserRepository(ChefConnectDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string hashedPassword)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.HashedPassword == hashedPassword);
        }


        // Authentication
        public async Task<User> AddAsync(User user)
        {
            // Check if email or username already exists
            if (await IsEmailExistsAsync(user.Email))
                throw new Exception("Email already exists.");

            if (await IsUsernameExistsAsync(user.Username))
                throw new Exception("Username already exists.");

            // Add user to the database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }

}
