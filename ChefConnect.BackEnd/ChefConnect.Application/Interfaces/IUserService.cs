using ChefConnect.Application.DTOs.User;
using ChefConnect.Domain.Common;

namespace ChefConnect.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDTO>> RegisterForCustomerAsync(string username, string email, string password);
    }
}
