using ContactBook_API.Models;
using ContactBook_API.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook_API.Controllers.CRUD
{
    [ApiController]
    [Route("api/[controller]")]
    public class Update_Put_UserByIdController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        public Update_Put_UserByIdController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        // PUT: /api/User/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] PutViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // Update user properties here based on the data in the model.
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = model.Password;
            user.UserName = model.UserName;


            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                // Handle update errors and return appropriate responses.
                return BadRequest(new { Message = "Failed to update user." });
            }

            return Ok(new { Message = "User updated successfully." });
        }


    }
}
