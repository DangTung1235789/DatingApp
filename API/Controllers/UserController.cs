using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //controller is get some data from our database
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsersController : ControllerBase
    {
        // DataContext:  dependency injection container
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        //endpoint: send back to the client
        [HttpGet]//getting data in this case
         
        //type of Action Results return this results (IEnumerable return list)
        //and send back to the client
        //async: task, await, TolistAsync, FindAsync
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return  await _context.Users.ToListAsync();
            
        }
        //https://localhost:5001/api/Users/3 => return AppUser(3)
        [HttpGet("{id}")]
         
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return  await _context.Users.FindAsync(id);          
        }

    }
}