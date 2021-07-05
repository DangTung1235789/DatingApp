using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
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
        //4. Using the pagination classes
        //update GetMembersAsync because we've added UserParams
        /*
        -   IQueryable: This is an expression that's going to go to our database or entity framework 
        is going to build up this query as an expression tree and then when we execute the 2 list, that's 
        when it goes and execute the request in our database
        - return PagedList of MemberDto truy cap PagedList.cs (CreateAsync: ctrl + f12)
        - Do thay đổi thuộc tính hàm interface IUserRepository nên phải thêm UserParams userParams
        */
        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            //7. Adding filtering to the API ( bộ lọc ) => Linq trong lập trình C# ngôn ngữ truy vấn tích hợp
            var query =  _context.Users.AsQueryable();
            //this will give us opportunity to do smt with this query and decide what we want to filter 
            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);
            // adding parameter to filter by the age of the user (2021 + -userParams.MaxAge - 1)
            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where( u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob); 

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                //we specify default in this switch statement is a _ 
                _ => query.OrderByDescending(u => u.LastActive)
            };
            //sending our query into a memberDto
            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.
                ConfigurationProvider).AsNoTracking(), 
                    userParams.PageNumber, userParams.PageSize);
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