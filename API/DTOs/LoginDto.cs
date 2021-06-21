namespace API.DTOs
{
    //Using DTOs (data transfer objects)
    //when we do send smt up in the body of request (register method), we have to send out as an object 
    //khoi tao obj de truyen ham in AccountController
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}