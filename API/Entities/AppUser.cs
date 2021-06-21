namespace API.Entities
{
    // create Entities
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        //public byte[] => it's going to return when we calculate the hash 
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}