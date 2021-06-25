using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    //4. Entity Framework relationships
    //có thể tạo 1 database để chứa photo , nhưng nhìn vào thiết kế app chúng ta ko muốn 1 database độc lập cho photo
    //like:  public DbSet<AppUser> Users { get; set; } in DataContext
    // we only want our photos to be added to a user's photo collection
    //I want the photo entity to be called photos in the database
    //specify photos as the table name
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        //add boolean property to see if this is their main photo 
        public bool IsMain { get; set; }
        //save us from adding a new migration when we get there is we're going to add another string 
        public string PublicId { get; set; }
        //this is what known as fully defining the reletionship between AppUser and Photo 
        public AppUser AppUser { get; set; }
        //this is what known as fully defining the reletionship between AppUser and Photo 
        public int AppUserId { get; set; }

    }
}