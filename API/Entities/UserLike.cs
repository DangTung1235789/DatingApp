namespace API.Entities
{
    public class UserLike
    {
        //2. Adding a likes entity
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public AppUser LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}