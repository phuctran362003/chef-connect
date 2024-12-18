using ChefConnect.Application.DTOs.User;
using ChefConnect.Application.DTOs.User.Authentication;
using ChefConnect.Application.Interfaces;
using ChefConnect.Domain.Common;
using ChefConnect.Domain.Entities;
using ChefConnect.Domain.Enums;
using ChefConnect.Infrastructure.Interfaces;
using ChefConnect.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace ChefConnect.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;



        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<ServiceResponse<UserDTO>> RegisterForCustomerAsync(string username, string email, string password)
        {
            try
            {
                if (await _unitOfWork.UserRepository.IsEmailExistsAsync(email))
                    return new ServiceResponse<UserDTO>("Email already exists.");

                if (await _unitOfWork.UserRepository.IsUsernameExistsAsync(username))
                    return new ServiceResponse<UserDTO>("Username already exists.");

                var hashedPassword = _passwordHasher.HashPassword(password);

                var newUser = new User
                {
                    Username = username,
                    Email = email,
                    HashedPassword = hashedPassword,
                    RoleId = 3, // Customer role
                    Status = UserStatus.Active
                };

                var registeredUser = await _unitOfWork.UserRepository.AddAsync(newUser);
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

        public async Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                    return new ServiceResponse<LoginResponse>("Email and Password are required.");

                // Hash the provided password
                var hashedPassword = HashPassword(loginRequest.Password);

                // Retrieve user by email and hashed password
                var user = await _unitOfWork.UserRepository.GetByEmailAndPasswordAsync(loginRequest.Email, hashedPassword);

                if (user == null)
                    return new ServiceResponse<LoginResponse>("Invalid email or password.");

                // Generate JWT tokens
                var tokenGenerator = new JwtTokenGenerator(_configuration);
                var accessToken = tokenGenerator.GenerateAccessToken(user);
                var refreshToken = tokenGenerator.GenerateRefreshToken();



                // Create login response
                var loginResponse = new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.RoleId.ToString()
                };

                return new ServiceResponse<LoginResponse>(loginResponse, "Login successful.");
            }
            catch (Exception ex)
            {
                return new ServiceResponse<LoginResponse>($"An error occurred during login: {ex.Message}");
            }
        }


        // Helper function to hash password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }










    }
}
