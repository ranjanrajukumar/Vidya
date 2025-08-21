using Microsoft.AspNetCore.Http;
using Vidya.Application.Interfaces;

namespace Vidya.Application.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        public string? Username => _httpContextAccessor.HttpContext?.Items["Username"]?.ToString();
    }
}
