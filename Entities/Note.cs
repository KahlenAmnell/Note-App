namespace Note_App_API.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorID { get; set; }
        public Dashboard Author { get; set; }
        public virtual Dashboard Dashboard { get; set; }

    }
}
