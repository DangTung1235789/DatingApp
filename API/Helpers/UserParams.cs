namespace API.Helpers
{   /*
    3. Adding helper classes for pagination:
    -   what we're going to do is add an additional header that contains the pagination information 
    that we can get that information out of the header in the client and then use that 
    to display the pagination information in the browser 
    */
    public class UserParams : PaginationParams
    {
        //going to set a maximum page size 
        //what's the most amount of things that we're going to return from a request
        
        //7. Adding filtering to the API ( bộ lọc )
        public string CurrentUsername { get; set; }
        public string Gender { get; set; }

        //8. Adding additional filters
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;
        //11. Adding sorting functionality
        public string OrderBy { get; set; } = "lastActive";
    }
}