using Microsoft.AspNetCore.Mvc;
using Note_App_API.Entities;
using Note_App_API.Services;

namespace Note_App_API.Controllers;
[ApiController]
[Route("api/note")]
public class NoteController : ControllerBase
{
    private readonly NoteDbContext _dbContext;
    private readonly INoteService _service;

    public NoteController(NoteDbContext dbContext, INoteService service)
    {
        _dbContext = dbContext;
        _service = service;
    }
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<Note>>> GetUserNotes([FromRoute] int userId)
    {
        var notes = await _service.GetAllUserNotesAsync(userId);

        return Ok(notes);
    }
}