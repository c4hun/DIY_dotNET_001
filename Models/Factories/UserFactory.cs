using Todo.Models.Interfaces;
using Todo.Models.Entities;

namespace Todo.Models.Factories;

public enum UserType { Admin, Visitor }

public static class UserFactory
{
    public static IUser CreateUser(UserType type, int id, string name)
    {
        return type switch
        {
            UserType.Admin => new AdminUser(id, name),
            UserType.Visitor => new VisitorUser(id, name),
            _ => throw new ArgumentException("Type inconnu")
        };
    }
}
