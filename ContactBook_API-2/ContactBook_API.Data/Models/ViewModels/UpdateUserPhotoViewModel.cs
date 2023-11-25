using Microsoft.AspNetCore.Http;

namespace ContactBook_API.Data.Models.ViewModels
{
    public class UpdateUserPhotoViewModel
    {
        public IFormFile Photo { get; set; }

    }
}
