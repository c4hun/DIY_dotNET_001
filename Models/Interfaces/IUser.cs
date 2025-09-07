namespace Todo.Models.Interfaces
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        void Notify(string message);
    }
}
