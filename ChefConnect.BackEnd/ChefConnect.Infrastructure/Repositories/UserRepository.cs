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

        // Method to handle user sign-up
        public async Task<User> CreateUserAsync(User newUser)
        {
            // Add user to the database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        // Method to handle user sign-in
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string hashedPassword)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.HashedPassword == hashedPassword);
        }

        public async Task<User> GetUserByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == emailOrUsername || u.Username == emailOrUsername);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }






    }

}
