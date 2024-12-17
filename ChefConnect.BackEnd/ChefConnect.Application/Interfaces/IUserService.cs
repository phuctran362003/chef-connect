using ChefConnect.Domain.Common;
using ChefConnect.Domain.Entities;

namespace ChefConnect.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> RegisterForCustomerAsync(string username, string email, string password);
    }
}
