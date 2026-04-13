using Drkb.UniversalBot.Application.Interfaces.Authorization;
using Microsoft.AspNetCore.Http;

namespace Drkb.UniversalBot.Infrastructure.Services.Authorization;

public class CurrentUserService: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("id")?.Value;
            return Guid.TryParse(userId, out Guid id) ? id : Guid.Empty;
        }
    }

    public List<string> Rights
    {
        get
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;
            if (claims == null) return new List<string>();

            return claims
                .Where(c => c.Type == "rights")
                .Select(c => c.Value)
                .ToList();
        }
    }

    public List<Guid> Departments
    {
        get
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;
            if (claims == null) return new List<Guid>();

            return claims
                .Where(c => c.Type == "departments")
                .Select(c => new Guid(c.Value))
                .ToList();
        }
    }
    
}