using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note_App_API.Entities;
using Note_App_API.Models;
using Note_App_API.Services;

namespace Note_App_API.Controllers;
[ApiController]
[Route("api/note")]
[Authorize]
public class NoteController : ControllerBase
{
    private readonly INoteService _service;

    public NoteController(INoteService service)
    {
        _service = service;
    }
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetUserNotes([FromRoute] int userId)
    {
        var notes = await _service.GetAllUserNotesAsync(userId);

        return Ok(notes);
    }

    [HttpPost("{userId}")]
    public async Task<ActionResult> CreateNote([FromRoute] int userId, [FromBody] CreateNoteDto newNote)
    {
        var noteId = await _service.CreateNewNote(userId, newNote);

        return Created($"{noteId}", null);
    }

    [HttpDelete("deleteNote/{noteId}")]
    public async Task<ActionResult> DeleteNote([FromRoute] int noteId)
    {
        await _service.Delete(noteId);
        return NoContent();
    }

    [HttpPut("update/{noteId}")]
    public async Task<ActionResult> UpdateNote([FromRoute] int noteId, [FromBody] CreateNoteDto dto)
    {
        await _service.Update(noteId, dto);
        return Ok();
    }
}