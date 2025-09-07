namespace Todo.Models.Entities
{
    public class AdminUser : BaseUser
    {
        public AdminUser(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override void Notify(string message)
        {
            Console.WriteLine($"[Admin] {Name} reçoit : {message}");
        }
    }
}
