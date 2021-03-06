namespace API.DTOs
{
    //Using DTOs (data transfer objects)
    //when we do send smt up in the body of request (register method), we have to send out as an object 
    //khoi tao obj de truyen ham in AccountController
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        //12. Adding the main photo image to the nav bar
        public string PhotoUrl { get; set; }
        //10. Updating the API register method
        public string KnownAs { get; set; }
        //9. Cleaning up the member service
        //send back another bit of information about the user when we log in
        //this is going to save us from making an API call simply to find out what gender the user is login
        public string Gender { get; set; }
    }
}