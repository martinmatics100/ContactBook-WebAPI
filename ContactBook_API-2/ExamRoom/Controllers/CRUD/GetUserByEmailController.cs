using ContactBook_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook_API.Controllers.CRUD
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetUserByEmailController : ControllerBase
    {




        private readonly UserManager<User> _userManager;
        public GetUserByEmailController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }




        // GET: /api/User/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserById(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // You might want to filter and return only specific user properties instead of returning the entire user object.
            return Ok($"Hurray!, User '{user.UserName}' was found.");
        }



    }
}

