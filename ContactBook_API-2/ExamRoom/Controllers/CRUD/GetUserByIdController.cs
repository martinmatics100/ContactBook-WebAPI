using ContactBook_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ContactBook_API.Controllers.CRUD
{

    [ApiController]
    [Route("api/[controller]")]
    public class GetUserByIdController : ControllerBase
    {


        private readonly UserManager<User> _userManager;
        public GetUserByIdController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }




        // GET: /api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // You might want to filter and return only specific user properties instead of returning the entire user object.
            return Ok($"Hurray!, User '{user.UserName}' was found.");
        }


    }
}
