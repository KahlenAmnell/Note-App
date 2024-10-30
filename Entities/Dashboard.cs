namespace Note_App_API.Entities
{
    public class Dashboard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User UserName { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
