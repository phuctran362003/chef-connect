using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationTestController : ControllerBase
    {
        [HttpGet("admin-only")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AdminOnly()
        {
            return Ok(new { Message = "Welcome, Admin! This endpoint is restricted to Admin users only." });
        }

        [HttpGet("chef-only")]
        [Authorize(Policy = "ChefOnly")]
        public IActionResult ChefOnly()
        {
            return Ok(new { Message = "Welcome, Chef! This endpoint is restricted to Chef users only." });
        }

        [HttpGet("customer-only")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult CustomerOnly()
        {
            return Ok(new { Message = "Welcome, Customer! This endpoint is restricted to Customer users only." });
        }

        [HttpGet("admin-or-chef")]
        [Authorize(Policy = "AdminOrChef")]
        public IActionResult AdminOrChef()
        {
            return Ok(new { Message = "Welcome! This endpoint is restricted to Admin or Chef users." });
        }

        [HttpGet("admin-chef-customer")]
        [Authorize(Policy = "AdminChefOrCustomer")]
        public IActionResult AdminChefOrCustomer()
        {
            return Ok(new { Message = "Welcome! This endpoint is available to Admin, Chef, or Customer users." });
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult Public()
        {
            return Ok(new { Message = "This endpoint is public and does not require authentication." });
        }
    }
}
