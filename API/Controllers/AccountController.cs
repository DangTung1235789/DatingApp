using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Math;

namespace API.Controllers
{
    //inheriting BaseApiController: [ApiController], [Route("api/[controller]")]
    //this is a controller basically set up   
    public class AccountController : BaseApiController
    {
        //16.7. Updating the account controller
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;

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
            //10. Updating the API register method
            var user = _mapper.Map<AppUser>(registerDto);
            //this is provide a hashing algorithm: use to create a password hash
            // using var hmac = new HMACSHA512();
            //create a new user 
            user.UserName = registerDto.Username.ToLower();
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if(!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                //call create token 
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                //9. Cleaning up the member service
                Gender = user.Gender
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
            var user = await _userManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); 

            if(!result.Succeeded) return Unauthorized();
            //we didn't find a user in our database with that username
            if (user == null)
            {
                return Unauthorized("Invalid username");
            }
            //calculate the computed hash of their password using the password solt
            // using var hmac = new HMACSHA512(user.PasswordSalt);

            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // for (int i = 0; i < computedHash.Length; i++)
            // {
            //     if (computedHash[i] != user.PasswordHash[i])
            //     {
            //         return Unauthorized("Invalid Password");
            //     }
            // }

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                //12. Adding the main photo image to the nav bar
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                //10. Updating the API register method
                KnownAs = user.KnownAs,
                //9. Cleaning up the member service
                Gender = user.Gender
            };
        }
        //check xem username da co trong database chua 
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}