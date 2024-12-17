using ChefConnect.Domain.Entities;
using ChefConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ChefConnect.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ChefConnectDbContext _context;

        public UserRepository(ChefConnectDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<User> RegisterUserAsync(User user, string password)
        {
            // Check if email or username already exists
            if (await IsEmailExistsAsync(user.Email))
                throw new Exception("Email already exists.");

            if (await IsUsernameExistsAsync(user.Username))
                throw new Exception("Username already exists.");

            // Hash the password before storing it
            user.HashedPassword = HashPassword(password);

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

        private string HashPassword(string password)
        {
            // Use SHA256 for password hashing
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
