using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefConnect.API.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [Authorize(Roles = "Customer")]
        // Endpoint to test JWT and Customer role
        [HttpGet("customer-role")]
        public IActionResult TestCustomerRole()
        {
            // Check if the JWT contains the "Customer" role
            var customerRoleClaim = User.Claims.FirstOrDefault(c => c.Type == "role" && c.Value == "Customer");

            if (customerRoleClaim == null)
            {
                return Unauthorized(new { Message = "You do not have the required role to access this endpoint." });
            }

            return Ok(new { Message = "JWT works! You have the Customer role." });
        }
    }
}
