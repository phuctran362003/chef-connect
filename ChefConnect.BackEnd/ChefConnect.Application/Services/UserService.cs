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

        public async Task<ServiceResponse<User>> RegisterForCustomerAsync(string username, string email, string password)
        {
            try
            {
                // Check for existing email or username
                if (await _unitOfWork.UserRepository.IsEmailExistsAsync(email))
                    return new ServiceResponse<User>("Email already exists.");

                if (await _unitOfWork.UserRepository.IsUsernameExistsAsync(username))
                    return new ServiceResponse<User>("Username already exists.");

                // Create the user object with RoleId = 3 (Customer role)
                var newUser = new User
                {
                    Username = username,
                    Email = email,
                    RoleId = 3, // Customer role
                    Status = UserStatus.Active
                };

                // Register the user
                var registeredUser = await _unitOfWork.UserRepository.RegisterUserAsync(newUser, password);

                // Save the transaction
                await _unitOfWork.SaveChangesAsync();

                return new ServiceResponse<User>(registeredUser, "Customer registered successfully.");
            }
            catch (Exception ex)
            {
                // Return failure response with error details
                return new ServiceResponse<User>($"An error occurred: {ex.Message}");
            }
        }
    }
}
