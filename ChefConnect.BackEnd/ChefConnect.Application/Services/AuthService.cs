using ChefConnect.Application.Common.Response;
using ChefConnect.Application.DTOs.User.Request;
using ChefConnect.Application.DTOs.User.Response;
using ChefConnect.Application.Interfaces;
using ChefConnect.Domain.Entities;
using ChefConnect.Domain.Enums;
using ChefConnect.Infrastructure.Interfaces;
using ChefConnect.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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


        //đăng kí
        public async Task<ServiceResponse<SignUpResponse>> SignUpAsync(SignUpRequest request)
        {
            _logger.LogInformation("SignUpAsync called with email: {Email}", request.Email);

            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Username))
                {
                    _logger.LogWarning("Invalid sign-up request: {Request}", request);
                    return new ServiceResponse<SignUpResponse>("Invalid input data.");
                }

                // Check if a user with the email already exists
                var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("Sign-up attempt with existing email: {Email}", request.Email);
                    return new ServiceResponse<SignUpResponse>("User with this email already exists.");
                }

                // Hash the password before saving
                var hashedPassword = HashPassword(request.Password);

                // Create a new user object
                var newUser = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    HashedPassword = hashedPassword,
                    RoleId = 2, // Default role (e.g., "User")
                    Status = UserStatus.Active
                };

                var createdUser = await _unitOfWork.UserRepository.CreateUserAsync(newUser);
                _logger.LogInformation("New user created: {Username}, {Email}", createdUser.Username, createdUser.Email);

                // Prepare the response
                var response = new SignUpResponse
                {
                    Username = createdUser.Username,
                    Email = createdUser.Email,
                    Message = "User signed up successfully."
                };

                return new ServiceResponse<SignUpResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during SignUpAsync for email: {Email}", request.Email);
                return new ServiceResponse<SignUpResponse>("An error occurred during sign-up.");
            }
        }

        //đăng nhập
        public async Task<ServiceResponse<TokenResponse>> SignInAsync(SignInRequest request)
        {
            _logger.LogInformation("SignInAsync called for email/username: {EmailOrUsername}", request.EmailOrUsername);

            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.EmailOrUsername) || string.IsNullOrWhiteSpace(request.Password))
                {
                    _logger.LogWarning("Invalid sign-in request: {Request}", request);
                    return new ServiceResponse<TokenResponse>("Invalid input data.");
                }

                // Hash the provided password
                var hashedPassword = HashPassword(request.Password);

                // Check for user by email or username
                var user = await _unitOfWork.UserRepository.GetUserByEmailOrUsernameAsync(request.EmailOrUsername, hashedPassword);
                if (user == null)
                {
                    _logger.LogWarning("Invalid sign-in attempt for email/username: {EmailOrUsername}", request.EmailOrUsername);
                    return new ServiceResponse<TokenResponse>("Invalid email/username or password.");
                }

                // Check if the user's account is active
                if (user.Status != UserStatus.Active)
                {
                    _logger.LogWarning("Inactive account sign-in attempt for email/username: {EmailOrUsername}", request.EmailOrUsername);
                    return new ServiceResponse<TokenResponse>("Account is inactive. Please contact support.");
                }

                // Generate tokens
                var tokenGenerator = new JwtTokenGenerator(_configuration);
                var accessToken = tokenGenerator.GenerateAccessToken(user);
                var refreshToken = tokenGenerator.GenerateRefreshToken();

                _logger.LogInformation("Tokens generated for user: {EmailOrUsername}", request.EmailOrUsername);

                // Prepare response
                var tokenResponse = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return new ServiceResponse<TokenResponse>(tokenResponse, "Sign-in successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during SignInAsync for email/username: {EmailOrUsername}", request.EmailOrUsername);
                return new ServiceResponse<TokenResponse>("An error occurred during sign-in.");
            }
        }





        // Utility method for password hashing
        private string HashPassword(string password)
        {
            _logger.LogInformation("Hashing password.");
            try
            {
                using var sha256 = System.Security.Cryptography.SHA256.Create();
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while hashing password.");
                throw;
            }
        }
    }
}
