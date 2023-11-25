using ContactBook_API.Data.Models;
using ContactBook_API.Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook_API.Core.Interface.CRUDInterface
{
    public interface ICrudRepository
    {
        Task<bool> CreateNewUserAsync(PostNewUserViewModel model, ModelStateDictionary modelState);
        Task<bool> UpdateUserAsync(string userId, PutViewModel model);
        Task<PaginatedUserViewModel> GetAllUsersAsync(int page, int pageSize);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(string userId);
        Task<bool> DeleteUserAsync(string userId);
        Task<string> UploadImageToCloudinaryAndSave(string userId, IFormFile file);
    }
}
