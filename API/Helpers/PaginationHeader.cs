namespace API.Helpers
{
    //3. Adding helper classes for pagination
    /*
    - creating our pagination information and what we're going to have is information about what's being paged and what we can return to our 
    client is the pagination information inside the header 
    - what we're going to do is add an additional header that contains the pagination information that we can get that information out of 
    the header in the client and then use that to display the pagination information in the browser
    */
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
        //this is all the information we want to send back to the client   
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}