using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Note_App_API.Entities;
using Note_App_API.Exceptions;
using Note_App_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Note_App_API.Services
{
    public interface IAccountService
    {
        Task<string> registerAccount(CreateAccountDto dto);
        string GenerateJwt(LoginDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly NoteDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(NoteDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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
        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user is null) throw new BadRequestException("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed) throw new BadRequestException("Invalid username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var day = _authenticationSettings.JwtExpireDays;
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_authenticationSettings.JwtExpireDays));

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            var newToken = tokenHandler.WriteToken(token);
            return newToken;
        }
    }
}
