using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContactBook_API.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string ImageUrl { get; set; }

    }
}
