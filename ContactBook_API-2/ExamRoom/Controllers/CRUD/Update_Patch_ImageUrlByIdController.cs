using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContactBook_API.Models;
//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;

namespace ContactBook_API.Controllers.CRUD
{
    public class Update_Patch_ImageUrlByIdController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public Update_Patch_ImageUrlByIdController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }




        //// PATCH: /api/User/photo/{id}
        //[HttpPatch("photo/{id}")]
        //public async Task<IActionResult> UpdateUserPhoto(string id, [FromForm] UpdateUserPhotoViewModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound(new { Message = "User not found." });
        //    }

        //    if (Request.Form.Files.Count > 0)
        //    {
        //        var file = Request.Form.Files[0];
        //        if (file.Length > 0)
        //        {
        //            // Initialize Cloudinary settings with your account details.
        //            //var cloudinary = new Cloudinary(new Account(
        //            //    "your_cloud_name",     // Replace with your Cloudinary cloud name
        //            //    "your_api_key",        // Replace with your Cloudinary API key
        //    "your_api_secret"      // Replace with your Cloudinary API secret
        //));

        //// Upload the file to Cloudinary.
        //var uploadParams = new ImageUploadParams
        //{
        //    File = new FileDescription(file.FileName, file.OpenReadStream()),
        //};

        //var uploadResult = cloudinary.Upload(uploadParams);

        //            // Check if the upload was successful.
        //            if (uploadResult.Error != null)
        //            {
        //                // Handle Cloudinary upload error and return appropriate responses.
        //                return BadRequest(new { Message = "Failed to upload user photo to Cloudinary." });
        //            }

        //            // Update the user's PhotoUrl property with the public URL of the uploaded image.
        //            user.PhotoUrl = uploadResult.Uri.ToString();
        //        }
        //    }

        //    var result = await _userManager.UpdateAsync(user);

        //    if (!result.Succeeded)
        //    {
        //        // Handle update errors and return appropriate responses.
        //        return BadRequest(new { Message = "Failed to update user photo." });
        //    }

        //    return Ok(new { Message = "User photo updated successfully." });
        //}


    }
}
