namespace Todo.Models.Entities
{
    public class VisitorUser : BaseUser
    {
        public VisitorUser(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override void Notify(string message)
        {
            Console.WriteLine($"[Visitor] {Name} reçoit : {message}");
        }
    }
}
