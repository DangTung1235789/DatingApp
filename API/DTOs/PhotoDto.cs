namespace API.DTOs
{
    public class PhotoDto
    {
        //(chúng ta sẽ thấy 1 vòng lặp AppUser return photo, photo return AppUser)
        //11. Adding a DTO for Members (giải quyết vấn đề vòng lặp)
        public int Id { get; set; }
        public string Url { get; set; }
        //add boolean property to see if this is their main photo 
        public bool IsMain { get; set; }
    }
}