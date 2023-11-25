using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ContactBook_API.Core.Interface;
using ContactBook_API.Core.Interface.AuthInterface;
using ContactBook_API.Core.Interface.CRUDInterface;
using ContactBook_API.Core.Repositories.CRUDRepository;
using ContactBook_API.Data.Models;
using ContactBook_API.Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook_API.Controllers.CRUD
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;
        private readonly UserManager<User> _userManager;
        public CRUDController(ICrudRepository crudRepository, UserManager<User> userManager)
        {
            _crudRepository = crudRepository;
            _userManager = userManager;
        }

        // POST: /api/User/add-new
        [HttpPost("add-new-user")]
        public async Task<IActionResult> AddNewUser([FromBody] PostNewUserViewModel model)
        {
            var userCreationResult = await _crudRepository.CreateNewUserAsync(model, ModelState);

            if (!userCreationResult)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { Message = "User created successfully." });
        }


        // PUT: /api/User/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] PutViewModel model)
        {
            var userUpdateResult = await _crudRepository.UpdateUserAsync(id, model);

            if (!userUpdateResult)
            {
                return BadRequest(new { Message = "Failed to update user or user not found." });
            }

            return Ok(new { Message = "User updated successfully." });
        }

        // GET: /api/User/all-users?Page=[current number]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10)
        {
            var paginatedResult = await _crudRepository.GetAllUsersAsync(page, pageSize);

            return Ok(paginatedResult);
        }

        // GET: /api/User/{email}
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _crudRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // You might want to filter and return only specific user properties instead of returning the entire user object.
            return Ok($"Hurray!, User '{user.UserName}' was found.");
        }

        // GET: /api/User/{id}
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _crudRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // You might want to filter and return only specific user properties instead of returning the entire user object.
            return Ok($"Hurray!, User '{user.UserName}' was found.");
        }

        // DELETE: /api/User/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userDeleteResult = await _crudRepository.DeleteUserAsync(id);

            if (!userDeleteResult)
            {
                return BadRequest(new { Message = "Failed to delete user or user not found." });
            }

            return Ok(new { Message = "User deleted successfully." });
        }


        //// PATCH: /api/User/photo/{id}
        //[HttpPatch("photo/{id}")]
        //public async Task<IActionResult> UploadUserImage(string id, IFormFile image)
        //{
        //    var user = await _userManager.FindByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound(new { Message = "User not found." });
        //    }

        //    if (image == null)
        //    {
        //        return BadRequest(new { Message = "Image file is required." });
        //    }

        //    if (image.Length <= 0)
        //    {
        //        return BadRequest(new { Message = "Image file is empty." });
        //    }

        //    // Upload the image to Cloudinary
        //    var cloudinary = new Cloudinary(new Account(
        //        "martinmatics123",
        //        "833589978836736",
        //        "TuECfDewamfIw49aElxBrMzy9Oo"
        //    ));

        //    var uploadParams = new ImageUploadParams
        //    {
        //        File = new FileDescription(image.FileName, image.OpenReadStream()),
        //    };

        //    var uploadResult = await cloudinary.UploadAsync(uploadParams);

        //    // Update the user's ImageUrl with the Cloudinary URL
        //    user.ImageUrl = uploadResult.SecureUri.AbsoluteUri;

        //    // Update the user in the database
        //    var updateResult = await _userManager.UpdateAsync(user);

        //    if (!updateResult.Succeeded)
        //    {
        //        // Handle update failure
        //        return BadRequest(new { Message = "Failed to update user's image." });
        //    }

        //    return Ok(new { Message = "User image updated successfully." });
        //}


        [HttpPatch("photo/{id}")]
        public async Task<IActionResult> UploadUserImage(string id, IFormFile image)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            if (image == null)
            {
                return BadRequest(new { Message = "Image file is required." });
            }

            // Use the injected image upload service to upload the image and update the user
            var uploadMessage = await _crudRepository.UploadImageToCloudinaryAndSave(id, image);

            if (uploadMessage != "File uploaded and saved successfully!")
            {
                return BadRequest(new { Message = uploadMessage });
            }

            return Ok(new { Message = "User image updated successfully." });
        }

    }
}
