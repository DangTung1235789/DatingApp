using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    //creating an interface and an implementation class for a repository 
    //the idea of this repository system is we can only provide the methods that we can to support for different entities
    //Creating a repository
    public interface IUserRepository 
    {
        //user update profile
         void Update(AppUser user);
         // this is save all our change
         Task<bool> SaveAllAsync();
         Task<IEnumerable<AppUser>> GetUsersAsync();
         //add a couple of different methods for getting a user 
         Task<AppUser> GetUserByIdAsync(int id);
         //get a user by the user name
         //we should do that return task is advise that they are Async 
         Task<AppUser> GetUserByUsernameAsync(string username);
         //15. Using AutoMapper queryable extensions
         //4. Using the pagination classes
         //return PagedList 
         Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
         Task<MemberDto> GetMemberAsync(string username);
    }
}