using API.Entities;

namespace API.Interfaces
{
    // lớp interface 
    public interface ITokenService
    {
         string CreateToken(AppUser user);
    }
}