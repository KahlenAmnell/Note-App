using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note_App_API.Entities;
using Note_App_API.Models;
using Note_App_API.Services;
using System.Security.Claims;

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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetUserNotes()
    {
        var notes = await _service.GetAllUserNotesAsync();

        return Ok(notes);
    }

    [HttpPost]
    public async Task<ActionResult> CreateNote([FromBody] CreateNoteDto newNote)
    {
        var noteId = await _service.CreateNewNote(newNote);

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