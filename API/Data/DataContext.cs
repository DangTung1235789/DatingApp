using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //DbContex represents the database and can be used to query
    //DataContext ten dai dien
    public class DataContext : DbContext
    {
        // create a constructor, use for startup confiuration
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        //DbSet create a database set for AppUser
        //User: database
        public DbSet<AppUser> Users { get; set; }
    }
}