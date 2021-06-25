using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
         public int Id { get; set; }
         //dung Username vì còn dùng trong Angular, AutoMap đủ thông minh để match trong AppUser
        public string Username { get; set; }
        //give this a photo 
        //our main photo we want to set to our main photo in our photo collection
        //we use this to send back for a user 
        public string PhotoUrl { get; set; }
        //Extending the user entity
        // Age sẽ gọi hàm getAge trongAppUser
        public int Age { get; set; }
        //kieu nhu nick name
        public string KnownAs { get; set; }
        //date time that their profile was created
        //give them some initial values
        //this is when they created their profile or they registered to our application
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; } 
        public string Gender { get; set; }
        /*
        - we'll use that information when it comes to displaying the members on the page will initially give
        them the option or display by default the members of the opposite gender
        */
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        //we add string to specify their interests
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // chúng ta se cho hết các prop ở AppUser trừ ICollection<Photo> 
        //we've got relationship between AppUser and Photo
        //1 User can have many photo 
        //(chúng ta sẽ thấy 1 vòng lặp AppUser return photo, photo return AppUser)
        //11. Adding a DTO for Members (giải quyết vấn đề vòng lặp)
        public ICollection<PhotoDto> Photos { get; set; }
    }
}