using ChefConnect.Domain.Entities;
using ChefConnect.Infrastructure.Interfaces;

namespace ChefConnect.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChefConnectDbContext _context;
        public IUserRepository UserRepository { get; private set; }
        public UnitOfWork(ChefConnectDbContext context, IUserRepository users)
        {
            _context = context;
            UserRepository = users;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
