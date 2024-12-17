using ChefConnect.API.Common;
using ChefConnect.Application.DTOs.User;
using ChefConnect.Application.Interfaces;
using ChefConnect.Domain.Common;
using ChefConnect.Infrastructure.DTOs.User.Register;
using Microsoft.AspNetCore.Mvc;

namespace ChefConnect.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new customer account.
        /// </summary>
        /// <param name="request">Customer registration details</param>
        /// <returns>ApiResponse with registration status</returns>
        [HttpPost("customer/register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>(new ServiceResponse<string>("Invalid input data.")));

            var response = await _userService.RegisterForCustomerAsync(request.Username, request.Email, request.Password);

            if (!response.IsSuccess)
                return BadRequest(new ApiResponse<UserDTO>(response));

            return Ok(new ApiResponse<UserDTO>(response));
        }
    }
}
