namespace Todo.Models.Request
{
    public class EndTaskRequest
    {
        public int Id { get; set; }
        public bool IsEnded { get; set; }
    }
}
