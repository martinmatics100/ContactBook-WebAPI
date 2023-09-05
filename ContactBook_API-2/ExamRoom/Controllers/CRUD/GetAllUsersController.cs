using ContactBook_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactBook_API.Models.ViewModels;

namespace ContactBook_API.Controllers.CRUD
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllUsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public GetAllUsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }



        // GET: /api/User/all-users?Page=[current number]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10)
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

            var paginatedResult = new PaginatedUserViewModel
            {
                TotalUsers = totalUsers,
                CurrentPage = page,
                PageSize = pageSize,
                Users = users
            };

            return Ok(paginatedResult);
        }

    }

}
