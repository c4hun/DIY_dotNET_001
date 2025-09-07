using Todo.Models.Interfaces;

namespace Todo.Models.Entities
{
    public abstract class BaseUser : IUser
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public abstract void Notify(string message);
    }
}
