using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using API.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API
{
    //ApplicationServiceExtensions inject this
    
    //StartUp class inject the data context into other parts of our application
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adding extension methods
            services.AddApplicationServices(_config);
            services.AddControllers();
            //Adding CORS support in the API
            services.AddCors(); 
            //Adding extension methods
            services.AddIdentityServices(_config);
            //17.2. Adding a presence hub
            services.AddSignalR();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //we see inside here is the first peace of middleware and exception 
            //handling middleware always come at the top of middleware container
            //because if an exception happens anywhere else in the middle where or as part of request 
            //then it gets thrown up to the next level of exception handling
            // if (env.IsDevelopment())
            // {
            //     //it's going to thrown all the way up to this 
            //     //bay gio nen su dung middleware de Exception (Bat loi, hien loi)
            //     app.UseDeveloperExceptionPage();
                
            // }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            
            app.UseRouting();
            //Adding CORS support in the API
            app.UseCors(x=>x.AllowAnyHeader()
                .AllowAnyMethod()
                //17.3. Authenticating to SignalR
                .AllowCredentials()
                .WithOrigins("https://localhost:4200"));
            //Adding the authentication middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //17.2. Adding a presence hub
                endpoints.MapHub<PresenceHub>("hubs/presence");
                //17.7. Creating a message hub
                endpoints.MapHub<MessageHub>("hubs/message");
            });
        }
    }
}
