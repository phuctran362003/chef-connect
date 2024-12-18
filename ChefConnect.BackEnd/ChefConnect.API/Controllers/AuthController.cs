using ChefConnect.API.Common;
using ChefConnect.Application.Common;
using ChefConnect.Application.DTOs.User;
using ChefConnect.Application.Interfaces;
using ChefConnect.Domain.Common;
using ChefConnect.Infrastructure.DTOs.User.Register;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChefConnect.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(new ServiceResponse<string>("Invalid input data.")));

            var response = await _userService.LoginAsync(request.Email, request.Password);

            if (!response.IsSuccess)
                return Unauthorized(new ApiResponse<TokenResponse>(response));

            return Ok(new ApiResponse<TokenResponse>(response));
        }


        /// <summary>
        /// Register a new customer account.
        /// </summary>
        /// <param name="request">Customer registration details</param>
        /// <returns>ApiResponse with registration status</returns>
        [HttpPost("customers")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ApiResponse<string>(new ServiceResponse<string>("Invalid input data.")));

                var response = await _userService.RegisterForCustomerAsync(request.Username, request.Email, request.Password);

                if (!response.IsSuccess)
                    return BadRequest(new ApiResponse<UserDTO>(response));

                return Ok(new ApiResponse<UserDTO>(response));
            }
            catch (Exception ex)
            {
                // Log the exception (assuming a logger is available)
                _logger.LogError(ex, "An error occurred while registering a new customer.");
                return StatusCode(500, new ApiResponse<string>(new ServiceResponse<string>("An unexpected error occurred.")));
            }
        }

    }
}
