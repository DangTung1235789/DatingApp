using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    //15. Adding extension methods, call in startup.cs
    public static class IdentityServiceExtensions
    {
        //"static" class that holds the extensions and then create static method
        //"IServiceCollection" tell it what we want to return from extension, it doesn't be to same type, it can be whatever u want
        //but this case, i want to return "services"
        // specify "this" before the type that you extending 
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //16.4. Configuring the startup class
            services.AddIdentityCore<AppUser>(opt => 
            {
                //tao mat khau manh
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<DataContext>();
            //Adding the authentication middleware
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            //16.10. Adding policy based authorisation
            services.AddAuthorization( opt => {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
            });
            return services;
        }
    }
}