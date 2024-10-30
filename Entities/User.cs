namespace Note_App_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Dashboard Dashboard { get; set; }
        public virtual List<Note> Notes { get; set; } = new List<Note>();
    }
}