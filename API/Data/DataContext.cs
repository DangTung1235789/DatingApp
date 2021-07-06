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
        //DbSet: create a database set for AppUser
        //User: database
        public DbSet<AppUser> Users { get; set; }
        //2. Adding a likes entity
        public DbSet<UserLike> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>()
                .HasKey(k => new {k.SourceUserId, k.LikedUserId} );

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}