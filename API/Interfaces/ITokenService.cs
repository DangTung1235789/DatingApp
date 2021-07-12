using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    // lớp interface 
    public interface ITokenService
    {
         Task<string> CreateToken(AppUser user);
    }
}