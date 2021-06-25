using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //Creating a repository
    //inject DataContext 
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            //this is what we use as our condition 
            //we want to get the user by user name 
            .Where(x => x.UserName == username)
            //when we pass ConfigurationProvider so we can go and get the configuration that we provided in our AutoMapperProfiles.cs
            //this tactic of projecting makes us more efficient in our database queries 
            //=> Go back UserController
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
            //we've got 20 properties in here, i'm not going the whole thing because mapper help us 
            //automatic give us the equivalent of doing this for every single property and it allow us to project inside our repository
            //it's only going to select the properties that we need actually need 
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            //Include(p => p.Photos): return Photo => neu ko gia tri se bi null
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //Include(p => p.Photos): return Photo => neu ko gia tri se bi null o postman
            return await _context.Users.Include(p => p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}