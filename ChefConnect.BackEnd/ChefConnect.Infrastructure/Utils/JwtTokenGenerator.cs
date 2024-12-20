using ChefConnect.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChefConnect.Infrastructure.Utils
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user, IEnumerable<Claim> additionalClaims = null)
        {
            // Retrieve the secret key from the configuration
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

            // Create default claims for the user
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, GetRoleNameById(user.RoleId)),
            new Claim("UserId", user.Id.ToString())
        };

            // Add any additional claims provided
            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            // Define token properties
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15), // Access token expiry (15 minutes)
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            // Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GetRoleNameById(int roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "Chef",
                3 => "Customer",
                _ => "Unknown"
            };
        }

        public string GenerateRefreshToken()
        {
            // Generate a refresh token using a Guid
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }

}
