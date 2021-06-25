using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        //we'll write the logic so that we can get the data out of JSON file and into database
        //we're not give Task any type parameter, but still give us asynchronous functionality even though we're return void
        //or not returning anything
        public static async Task SeedUsers(DataContext context)
        {   
            //we want to check if i use this table contain any user
            //it will return if we do have any user
            if(await context.Users.AnyAsync()){
                return;
            }
            //if we dont have any users in our database 
            //we want to go and interrogate that file to see what we have inside there and store it in a variable
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            //users should be a normal list of users of type AppUser
            var users =  JsonSerializer.Deserialize<List<AppUser>>(userData);   
            foreach (var user in users)
            {
                //we can go add them to our database 
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                //password for all of these Users
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user); 
            }
            //what we doing with database after we've looped through all of the users
            await context.SaveChangesAsync();
            //this is our Seed method 
        }
    }
}