using ContactBook_API.Data.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook_API.Core.Interface.AuthInterface
{
    public interface IAuthRepository
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model, ModelStateDictionary modelState);
        Task<string> LoginAsync(LoginViewModel model);
    }
}
