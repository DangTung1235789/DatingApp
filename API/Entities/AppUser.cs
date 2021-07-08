using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    // create Entities
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        //public byte[] => it's going to return when we calculate the hash 
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        //Extending the user entity
        public DateTime DateOfBirth { get; set; }
        //kieu nhu nick name
        public string KnownAs { get; set; }
        //date time that their profile was created
        //give them some initial values
        //this is when they created their profile or they registered to our application
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
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
        public ICollection<Photo> Photos { get; set; }
        // public int GetAge()
        // {
        //     // call function in DateTimeExtension to Calculate Age
        //     return DateOfBirth.CalculateAge();
        // }
        //2. Adding a likes entity
        public ICollection<UserLike> LikedByUsers { get; set; }
        public ICollection<UserLike> LikedUsers { get; set; }

        // additional 2 collection 
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
    }
}