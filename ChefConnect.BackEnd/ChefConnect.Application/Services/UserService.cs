using ChefConnect.Application.Interfaces;
using ChefConnect.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ChefConnect.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





    }
}
