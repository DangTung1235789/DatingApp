using System.Collections.Generic;
using System.Linq;
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

    }
}