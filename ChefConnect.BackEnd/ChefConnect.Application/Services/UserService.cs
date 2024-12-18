using ChefConnect.Application.Common;
using ChefConnect.Application.DTOs.User;
using ChefConnect.Application.Interfaces;
using ChefConnect.Domain.Common;
using ChefConnect.Domain.Entities;
using ChefConnect.Domain.Enums;
using ChefConnect.Infrastructure.Interfaces;
using ChefConnect.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ChefConnect.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<ServiceResponse<TokenResponse>> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
                if (user == null || !VerifyPassword(password, user.HashedPassword))
                    return new ServiceResponse<TokenResponse>("Invalid email or password.");

                // Generate tokens
                var jwtGenerator = new JwtTokenGenerator(_configuration);
                var accessToken = jwtGenerator.GenerateAccessToken(user);
                var refreshToken = jwtGenerator.GenerateRefreshToken();

                // (Optional) Store the refresh token in the database if needed

                var tokenResponse = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return new ServiceResponse<TokenResponse>(tokenResponse, "Login successful.");
            }
            catch (Exception ex)
            {
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

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            // Hash the input password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);

                // Convert hashed bytes to a base64 string
                string hashedInputPassword = Convert.ToBase64String(hashedBytes);

                // Compare the hashed input password with the stored hashed password
                return hashedInputPassword == storedHashedPassword;
            }
        }

    }
}
