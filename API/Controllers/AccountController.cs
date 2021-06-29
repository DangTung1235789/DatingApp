using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //inheriting BaseApiController: [ApiController], [Route("api/[controller]")]
    //this is a controller basically set up   
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;

        private readonly DataContext _context;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }
        //create the method to register a new user
        //use "Post" if we want to add a new resource through our API endpoint
        [HttpPost("register")]
        //asynchronous task: we're going to do smt with our database
        //"ActionResult" return from any of our control and methods
        //specify the type of thing: AppUser (UserDto vi co token)       
        //call a method "Register"
        //them registerDTO la obj de truyen vao method (ko dung string de truyen)
        //luc nhap tren postman la truyen tham so vao registerDto 
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDto)
        {
            //neu ton tai tai khoan trung thi tra ve "Username is taken"
            //await use: asynchronous task: we're going to do smt with our database
            if (await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }
            //this is provide a hashing algorithm: use to create a password hash
            using var hmac = new HMACSHA512();
            //create a new user 
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            // add User register to Database 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //retrun UserDto
            return new UserDto
            {
                Username =  user.UserName,
                //call create token 
                Token = _tokenService.CreateToken(user)
            };
        }
        //create the method to "login" 
        //use "Post" if we want to add a new resource through our API endpoint
        [HttpPost("login")]
        //asynchronous task: we're going to do smt with our database
        //"ActionResult" return from any of our control and methods
        //specify the type of thing: AppUser (UserDto vi co token)       
        //call a method "Register"
        //them LoginDto la obj de truyen vao method (ko dung string de truyen)
        //luc nhap tren postman la truyen tham so vao LoginDto 
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //we're going to make a reuest to our database => await
            //find an entity in our database, we used to FindAsync() -> primary key
            //Username not primary key in our database => use SingleOrDefaultAsync 
            var user = await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            //we didn't find a user in our database with that username
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }
            //calculate the computed hash of their password using the password solt
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            //retrun UserDto
            return new UserDto
            {
                Username =  user.UserName,
                Token = _tokenService.CreateToken(user),
                //12. Adding the main photo image to the nav bar
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
            };
        }
        //check xem username da co trong database chua 
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}