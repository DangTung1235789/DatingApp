using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
         IUserRepository UserRepository { get; }
         IMessageRepository MessageRepository {get; }
         ILikesRepository LikesRepository {get; }
         Task<bool> Complete(); //method to save all our changes, call it when complete
         bool HasChange(); //if EF has been tracking or has any change 
    }
}