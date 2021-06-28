using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //controller is get some data from our database
    //all of the methods inside this controller are going to be protected with Authorize 
    [Authorize]
    public class UsersController : BaseApiController
    {
        // DataContext:  dependency injection container
        //private readonly DataContext _context;
        // public UsersController(DataContext context)
        // {
        //     _context = context;
        // }
        private readonly IUserRepository _userRepository;
        //we're going to use is IUserRepository 
        //now we got _mapper to access to map
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        //endpoint: send back to the client
        [HttpGet]//getting data in this case
        //Adding the authentication middleware
        //[AllowAnonymous]: remove this because we do want authentication on our user control
        //type of Action Results return this results (IEnumerable return list)
        //and send back to the client
        //async: task, await, TolistAsync, FindAsync
        //return list users
        //ko dung AppUser vi tao vong lap
        public async Task<ActionResult<IEnumerable</*AppUser*/MemberDto>>> GetUsers()
        {
            //return await _context.Users.ToListAsync();
            ////we're going to use is IUserRepository 
            // var users = await _userRepository.GetUsersAsync();
            //we need to inject the map interface that we got from IMapper
            //we can specify what we want to map to IEnumerable of member DTO, then we pass at the source obj 
            // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }
        //https://localhost:5001/api/Users/3 => return AppUser(3)
        //Adding the authentication middleware
        //[Authorize]: remove this because we do want authentication on our user control
        [HttpGet("{username}")]

        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            // we're going to be return is MemberDto and Mapper is take care of all of the mapping 
            //between our AppUser and the MemberDto
            //var user = await _userRepository.GetUserByUsernameAsync(username);
            //we getting the user instead of GetUserByUsernameAsync => GetMemberAsync
            //we return a member from our repository 
            return await _userRepository.GetMemberAsync(username);
            //return _mapper.Map<MemberDto>(user);
        }
        //6. Persisting the changes in the API 
        //this is the kind of method that we use to update a resource on server
        [HttpPut]
        /*
            - But we don't need to send anything, any obj back from this, because the theory is that the 
            client has all the data related to the entity we're about to update 
            - So we don't need to return the obj back from APi because the client is telling the API what
            it's updating   
        */
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            /*
            - we want to do when we update a user, we need to get hold of the user and we also need to 
            get hold of the user's username
            - we don't want to trust the user give us their user name
            - we want to get it from what we're authenticating again, which is the token 
            */
            //this should give us is the user's username from the token that the API uses to authenticate this user
            //so that's the user we're going to be updating in this case
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);
            /*
            - When we're updating or using this to update an obj, then what we can use this method (map)
            - this "map" method has got 10 different overloads, options what we can pass into this
            - the overloads that we're going to use allows us to pass in the member update DTOs
            - and then we specify what we going to map it to (user) 
            */
            //this's save us mapping between our updating and our user obj 
            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);
            //we don't need to send any content back for a request
            if (await _userRepository.SaveAllAsync()) return NoContent();
            
            return BadRequest("Failed to update user");
        }
    }
}