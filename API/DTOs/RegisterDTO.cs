using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    //Using DTOs (data transfer objects)
    //when we do send smt up in the body of request (register method), we have to send out as an object 
    //khoi tao obj de truyen ham in AccountController
    public class RegisterDTO
    {
        //tranh dat ten UserName vi trung vi Entities
        [Required] public string Username { get; set; }
        
        //10. Updating the API register method
        [Required] public string KnownAs { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }

        [Required]
        //7. Error handling
        //we've got some additional things that are output 
        //if we don't meet some validation requirements
        //khi dang nhap thi do dai nho nhat la 4 max la 8
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}