using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Note_App_API.Authorization;
using Note_App_API.Entities;
using Note_App_API.Exceptions;
using Note_App_API.Models;

namespace Note_App_API.Services
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllUserNotesAsync(int userId);
        Task<int> CreateNewNote(int userId, CreateNoteDto dto);
        Task Delete(int noteId);
        Task Update(int noteId, CreateNoteDto dto);
    }

    public class NoteService : INoteService
    {
        private readonly NoteDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private IUserContextService _userContextService;

        public NoteService(NoteDbContext dbContext, IMapper mapper, ILogger<NoteService> logger, 
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return note.Id;
        }

        public async Task Delete(int noteId)
        {
            _logger.LogError($"Note with id: {noteId} DELETE action invoked");
            var note = _dbContext
                .Notes
                .FirstOrDefault(n => n.Id == noteId);

            if (note == null) throw new NotFoundException("Note not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, note,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            _dbContext.Notes.Remove(note);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int noteId, CreateNoteDto dto)
        {
            var note = _dbContext
                .Notes
                .FirstOrDefault(n => n.Id == noteId);

            if (note == null) throw new NotFoundException("Note not found");

            note.Title = dto.Title;
            note.Content = dto.Content;
            await _dbContext.SaveChangesAsync();

        }
    }
}
