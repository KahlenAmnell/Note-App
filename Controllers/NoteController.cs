using Microsoft.AspNetCore.Mvc;
using Note_App_API.Entities;

namespace Note_App_API.Controllers;
[Route("api/note")]
public class NoteController : ControllerBase
{
    private readonly NoteDbContext _dbContext;

    public NoteController(NoteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ActionResult<IEnumerable<Note>> GetUserNotes()
    {

    }
}