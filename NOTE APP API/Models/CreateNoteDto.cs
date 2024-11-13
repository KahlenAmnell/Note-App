using System.ComponentModel.DataAnnotations;

namespace Note_App_API.Models
{
    public class CreateNoteDto
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(450)]
        public string Content { get; set; }
    }
}
