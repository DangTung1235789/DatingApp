using System.Text.Json;
using API.Helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    // Extensions => static
    //adding our pagination headers onto response
    public static class HttpExtensions
    {
        /*
        - we need to create a class so that we can receive the pagination parameters from the users, because 
        in our user controller have GetUsers() is take all of these parameter as query string and we're 
        going to store that in an obj  => UserParams.cs
        */
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, 
            int itemsPerPage, int totalItems, int totalPages)
        {
            //create a pagination header 
            //ép kiểu về JSON camel Case
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            //and we want to add our pagination to our response header
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            // we need to add a cause header onto this to make this header available
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}