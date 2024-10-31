using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Note_App_API.Entities;
using Note_App_API.Models;

namespace Note_App_API.Services
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllUserNotesAsync(int userId);
        Task<int> CreateNewNote(int userId, CreateNoteDto dto);
    }

    public class NoteService : INoteService
    {
        private readonly NoteDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;

        public NoteService(NoteDbContext dbContext, IMapper mapper, ILogger<NoteService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<NoteDto>> GetAllUserNotesAsync(int userId)
        {
            var notes = await _dbContext
                .Notes
                .Where(n => n.AuthorID == userId)
                .ToListAsync();


            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        public async Task<int> CreateNewNote(int userId, CreateNoteDto dto)
        {
            var note = _mapper.Map<Note>(dto);
            note.AuthorID = userId;
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();

            return note.Id;
        }
    }
}
