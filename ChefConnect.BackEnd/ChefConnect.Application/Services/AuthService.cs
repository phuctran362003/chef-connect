using ChefConnect.Application.Common;
using ChefConnect.Application.DTOs.User;
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
        private readonly IPasswordHasher _passwordHasher;



        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AuthService> logger, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResponse<TokenResponse>> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogWarning("No user found with email: {Email}", email);
                    return new ServiceResponse<TokenResponse>("Invalid email or password.");
                }

                // Generate tokens
                _logger.LogDebug("Generating access token for user: {UserId}", user.Id);
                var jwtGenerator = new JwtTokenGenerator(_configuration);
                var accessToken = jwtGenerator.GenerateAccessToken(user);
                _logger.LogDebug("Access Token: {AccessToken}", accessToken);
                var refreshToken = jwtGenerator.GenerateRefreshToken();

                var tokenResponse = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                _logger.LogInformation("User {Email} logged in successfully.", email);
                return new ServiceResponse<TokenResponse>(tokenResponse, "Login successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for email: {Email}", email);
                return new ServiceResponse<TokenResponse>($"An error occurred: {ex.Message}");
            }
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





        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                    byte[] hashedBytes = sha256.ComputeHash(inputBytes);

                    string hashedInputPassword = Convert.ToBase64String(hashedBytes);

                    // Log for debugging purposes
                    _logger.LogDebug("Input Password Hash: {HashedInputPassword}", hashedInputPassword);

                    return hashedInputPassword == storedHashedPassword;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyPassword");
                return false;
            }
        }

    }
}
