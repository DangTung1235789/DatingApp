namespace API.DTOs
{
    public class MemberUpdateDto
    {
        //because it's a DTOs and we're going to want to map this into our user entity 
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}