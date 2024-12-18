using ChefConnect.Application.DTOs.User;
using ChefConnect.Application.DTOs.User.Authentication;
using ChefConnect.Domain.Common;

namespace ChefConnect.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        Task<ServiceResponse<UserDTO>> RegisterForCustomerAsync(string username, string email, string password);
    }
}
