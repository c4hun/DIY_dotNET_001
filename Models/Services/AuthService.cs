using Todo.Models.Repositories.Interfaces;  // Pour IUserRepository
using Todo.Models.Interfaces;              // Pour IUser

namespace Todo.Models.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Username == username);
            return user != null && user.Password == password;
        }
    }
}
