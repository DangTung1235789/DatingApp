using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        //we bring ILogger so that we can still logout our exception into the terminal
        //we want to display it in our terminal windows where we're running (dotnet watch run)
        //this is useful information
        //IHostEnvironment: we want to check environment we're running 
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, 
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }
        //we're going to give this middleware its required method 
        //this is happening in the context of the HTTP request when we'll add where we've access 
        //to the actual HTTP request that's coming in 
        public async Task InvokeAsync(HttpContext context)
        {
            /* 
                - if any of them get an exception they're going to throw the exception up and up until 
                they reach smt that can handle the exception 
                - because our exception middleware is going to be at top of that tree, then we're going 
                to catch the exception inside here
            */
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //we want to see as much information about what's happend with any exception
                context.Response.ContentType = "application/json";
                // ep kieu 500 error
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                // ? la co the null
                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");
                //we going to give it property name policy 
                //this is ensure our response just goes back as normal Json format response in camel case
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
                //this is our middleware 
            } 
        }
    }
}