using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Note_App_API.Entities;
using Note_App_API.Models;

namespace Note_App_API.Services
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllUserNotesAsync(int userId);
    }

    public class NoteService : INoteService
    {
        private readonly NoteDbContext _dbContext;
        private readonly IMapper _mapper;

        public NoteService(NoteDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
    }
}
