using Todo.Models.Interfaces;
using Todo.Models.Entities;

namespace Todo.Models.Factories;

public enum UserType { Admin, Visitor }

// Extraire des constantes ou une méthode pour la validation: ce la permettrait de préparer le code pour de futurs types d'utilisateurs.
public static class UserFactory
{
    public static IUser CreateUser(UserType type, int id, string name)
    {
        return type switch
        {
            UserType.Admin => CreateAdmin(id, name),
            UserType.Visitor => CreateVisitor(id, name),
            _ => throw new ArgumentException($"Type utilisateur inconnu : {type}")
        };
    }

    private static IUser CreateAdmin(int id, string name) => new AdminUser(id, name);
    private static IUser CreateVisitor(int id, string name) => new VisitorUser(id, name);
}

