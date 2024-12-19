using ChefConnect.Application.Common.Response;
using ChefConnect.Application.DTOs.User.Request;
using ChefConnect.Application.DTOs.User.Response;

namespace ChefConnect.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<TokenResponse>> SignInAsync(SignInRequest request);
        Task<ServiceResponse<SignUpResponse>> SignUpAsync(SignUpRequest request);
    }
}
