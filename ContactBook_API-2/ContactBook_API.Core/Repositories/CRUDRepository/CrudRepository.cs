using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContactBook_API.Core.Interface.CRUDInterface;
using ContactBook_API.Data.Models;
using ContactBook_API.Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook_API.Core.Repositories.CRUDRepository
{
    public class CrudRepository : ICrudRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly Cloudinary _cloudinary;

        public CrudRepository(UserManager<User> userManager, Cloudinary cloudinary)
        {
            _userManager = userManager;
            _cloudinary = cloudinary;
        }

        public async Task<bool> CreateNewUserAsync(PostNewUserViewModel model, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return false;
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                ImageUrl = "default-image-url.jpg" // Set a default image URL
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(string.Empty, error.Description);
                }

                return false;
            }

            return true;
        }


        public async Task<bool> UpdateUserAsync(string userId, PutViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false; // User not found
            }

            // Update user properties based on the data in the model.
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = model.Password;
            user.UserName = model.UserName;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }


        public async Task<PaginatedUserViewModel> GetAllUsersAsync(int page, int pageSize)
        {
            var totalUsers = await _userManager.Users.CountAsync();
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            // Ensure the page number is within a valid range.
            page = Math.Max(1, Math.Min(totalPages, page));

            var users = await _userManager.Users
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedUserViewModel
            {
                TotalUsers = totalUsers,
                CurrentPage = page,
                PageSize = pageSize,
                Users = users
            };
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false; // User not found
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<string> UploadImageToCloudinaryAndSave(string userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "Invalid file data.";
            }

            const int maxFileSizeInBytes = 300 * 1024;
            if (file.Length > maxFileSizeInBytes)
            {
                return "File size exceeds the maximum limit (300KB).";
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return "Only jpg and png files are allowed.";
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "User not found.";
            }

            // Upload the file/image to Cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                return "Error uploading image to Cloudinary.";
            }

            // Save Cloudinary URL to the user's ImageUrl column
            user.ImageUrl = uploadResult.Uri.ToString();

            // Update the user in the database
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return "Failed to update user's image.";
            }

            return "File uploaded and saved successfully!";
        }

    }
}
