using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        //we'll write the logic so that we can get the data out of JSON file and into database
        //we're not give Task any type parameter, but still give us asynchronous functionality even though we're return void
        //or not returning anything
        //16.6. Updating the seed method
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {   
            //we want to check if i use this table contain any user
            //it will return if we do have any user
            if(await userManager.Users.AnyAsync()){
                return;
            }
            //if we dont have any users in our database 
            //we want to go and interrogate that file to see what we have inside there and store it in a variable
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            //users should be a normal list of users of type AppUser
            var users =  JsonSerializer.Deserialize<List<AppUser>>(userData);  

            //16.8. Adding roles to the app
            if(users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{ Name = "Member"},
                new AppRole{ Name = "Admin"},
                new AppRole{ Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                //we can go add them to our database 
                // using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                //password for all of these Users
                // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                // user.PasswordSalt = hmac.Key;

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }
            //what we doing with database after we've looped through all of the users
            // await context.SaveChangesAsync();
            //this is our Seed method 

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
        }
    }
}