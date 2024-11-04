using Microsoft.AspNetCore.Identity;
using Note_App_API.Entities;
using Note_App_API.Models;

namespace Note_App_API.Services
{
    public interface IAccountService
    {
        Task<string> registerAccount(CreateAccountDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly NoteDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(NoteDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> registerAccount(CreateAccountDto dto)
        {
            var newUser = new User
            {
                Email = dto.Email,
                Name = dto.Name
            };
            newUser.Password = _passwordHasher.HashPassword(newUser, dto.Password);

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return newUser.Name;
        }
    }
}
