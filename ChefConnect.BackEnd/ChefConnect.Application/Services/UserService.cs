using ChefConnect.Application.DTOs.User;
using ChefConnect.Application.Interfaces;
using ChefConnect.Domain.Common;
using ChefConnect.Domain.Entities;
using ChefConnect.Domain.Enums;
using ChefConnect.Infrastructure.Interfaces;

namespace ChefConnect.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<UserDTO>> RegisterForCustomerAsync(string username, string email, string password)
        {
            try
            {
                if (await _unitOfWork.UserRepository.IsEmailExistsAsync(email))
                    return new ServiceResponse<UserDTO>("Email already exists.");

                if (await _unitOfWork.UserRepository.IsUsernameExistsAsync(username))
                    return new ServiceResponse<UserDTO>("Username already exists.");

                var newUser = new User
                {
                    Username = username,
                    Email = email,
                    RoleId = 3, // Customer role
                    Status = UserStatus.Active
                };

                var registeredUser = await _unitOfWork.UserRepository.RegisterUserAsync(newUser, password);
                await _unitOfWork.SaveChangesAsync();

                // Map to UserDTO
                var userDTO = new UserDTO
                {
                    Username = registeredUser.Username,
                    Email = registeredUser.Email
                };

                return new ServiceResponse<UserDTO>(userDTO, "Customer registered successfully.");
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UserDTO>($"An error occurred: {ex.Message}");
            }
        }
    }
}
