using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        //this to seed our data into our database
        //instead returning void, we're going to return async and task 
        public static async Task Main(string[] args)
        {
            //what goes inside here happens before our application is actually started 
            //5. Generating seed data
            var host = CreateHostBuilder(args).Build();
            //what we need get our sevice, our context service so that we can pass it to our seed method
            //we're creating a scope for the services that we're going to create in this part
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            //setting up a global exception handler, we dont have access to it in this method
            //16.6. Updating the seed method
            try
            {
                var context = services.GetRequiredService<DataContext>();
                /*
                what we're going to do in the future is just restart our application to apply any migrations
                what this also mean if we drop our database then all we need to do is restart our application
                and our database recreated 
                */
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                await context.Database.MigrateAsync();
                await Seed.SeedUsers(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
