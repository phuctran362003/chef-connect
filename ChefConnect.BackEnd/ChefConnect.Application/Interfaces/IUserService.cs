using ChefConnect.Application.Common;
using ChefConnect.Application.DTOs.User;
using ChefConnect.Domain.Common;

namespace ChefConnect.Application.Interfaces
{
    public interface IUserService
    {

        Task<ServiceResponse<TokenResponse>> LoginAsync(string email, string password);
        Task<ServiceResponse<UserDTO>> RegisterForCustomerAsync(string username, string email, string password);
    }
}
