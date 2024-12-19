using ChefConnect.Application.Common.Response;
using ChefConnect.Application.DTOs.User.Request;
using ChefConnect.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChefConnect.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Endpoint for Sign-Up
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ServiceResponse<string>("Invalid request data."));

                var response = await _authService.SignUpAsync(request);
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                var errorResponse = new ServiceResponse<string>("An internal server error occurred.");
                return StatusCode(500, errorResponse);
            }
        }

        // Endpoint for Sign-In
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ServiceResponse<string>("Invalid request data."));

                var response = await _authService.SignInAsync(request);
                return response.IsSuccess ? Ok(response) : Unauthorized(response);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                var errorResponse = new ServiceResponse<string>("An internal server error occurred.");
                return StatusCode(500, errorResponse);
            }
        }
    }




}
