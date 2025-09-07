using Todo.Models.Interfaces;

namespace Todo.Models.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(IUser user);
        IEnumerable<IUser> GetAllUsers();
    }
}
