using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Note_App_API.Entities;

namespace Note_App_API.Services
{
    public interface IAccountService
    {

    }
    public class AccountService : IAccountService
    {
        private readonly NoteDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;

        public AccountService(NoteDbContext dbContext, IMapper mapper, ILogger<NoteService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
