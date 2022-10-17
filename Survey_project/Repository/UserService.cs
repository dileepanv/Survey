using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Survey_project.Repository
{
    public class UserService : IUserServices
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public int UserID => int.Parse(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


        public string Email => _httpContext.HttpContext.User.FindFirst(ClaimTypes.Email).Value;


        public string Name => _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
    }
}
