using CloudinaryDotNet;
using ContactBook_API.Core.Interface;
using ContactBook_API.Core.Interface.AuthInterface;
using ContactBook_API.Core.Interface.CRUDInterface;
using ContactBook_API.Core.Repositories;
using ContactBook_API.Core.Repositories.AuthRepository;
using ContactBook_API.Core.Repositories.CRUDRepository;
using ContactBook_API.Data.Models;
using ContactBook_API.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactBook_API.Extension
{
    public static class DbRegistryExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Register your DbContext with the DI container
            services.AddDbContext<MartinDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            var cloudinarySettings = configuration.GetSection("Cloudinary");

            var account = new Account(
                cloudinarySettings["CloudName"],
                cloudinarySettings["ApiKey"],
                cloudinarySettings["ApiSecret"]);

            var cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);

            //// Configure and register CloudinaryService with Cloudinary credentials
            //var cloudinarySettings = configuration.GetSection("Cloudinary").Get<CloudinarySetting>();
            //var cloudinary = new Cloudinary(new Account(
            //    cloudinarySettings.CloudName,
            //    cloudinarySettings.ApiKey,
            //    cloudinarySettings.ApiSecret
            //));

            //services.AddSingleton(cloudinary);


            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICrudRepository, CrudRepository>();
            
            

        }
    }
}
