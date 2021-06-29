using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using API.Helpers;

namespace API.Extensions
{
    //15. Adding extension methods, call in startup.cs
    public static class ApplicationServiceExtensions
    {
        //"static" class that holds the extensions and then create static method
        //"IServiceCollection" tell it what we want to return from extension, it doesn't be to same type, it can be whatever u want
        //but this case, i want to return "services"
        // specify "this" before the type that you extending 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //3. Configuring cloudinary in the API (appsettings.json)
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //4. Adding a photo service
            services.AddScoped<IPhotoService, PhotoService>();
            //testing is the main reason for creating an interface
            //add lifetime of token
            services.AddScoped<ITokenService, TokenService>();
            //inside here we want to add the service for our repository
            //ket noi IUserRepository voi UserRepository
            services.AddScoped<IUserRepository, UserRepository>();
            //add automapper de giai quyet loi vong lap 
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly); //check loi
            //create a connection string for our database
            //connection string or other connection string for SQLite 
            //so that we can connect to our database from our application
            //the way that we do this typically is we add this to our configuration files(.json)
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}