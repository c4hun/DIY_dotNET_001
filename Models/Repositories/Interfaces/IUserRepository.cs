using Todo.Models.Repositories.Interfaces;
using Todo.Models.Services;

namespace Todo.Models.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(IUser user);
        IEnumerable<IUser> GetAllUsers();
    }
}
