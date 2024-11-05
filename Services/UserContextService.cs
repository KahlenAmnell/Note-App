using System.Security.Claims;

namespace Note_App_API.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
    public class UserContextService : IUserContextService
    {
        private IHttpContextAccessor _httpContextAnccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAnccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAnccessor.HttpContext?.User;
        public int? GetUserId =>
            User is null ?
            null :
            (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
