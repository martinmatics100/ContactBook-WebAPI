using ContactBook_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using ContactBook_API.Data.Models;
using ContactBook_API.Data.Models.ViewModels;
using ContactBook_API.Core.Interface.AuthInterface;

namespace ContactBook_API.Controllers.AuthController
{


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _registerRepository;

        public AuthController(IAuthRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var registrationResult = await _registerRepository.RegisterUserAsync(model, ModelState);

            if (!registrationResult)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(new { Message = "User registration successful." });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            // Validate the request model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Attempt to log in using the AuthRepository
            var token = await _registerRepository.LoginAsync(model);

            if (token == null)
            {
                // Login failed or user not found
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Login successful, return the JWT token
            return Ok(new { Token = token });
        }
    }
}
