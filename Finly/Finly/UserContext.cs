using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Finly;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var value = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (value == null)
                throw new UnauthorizedAccessException("User ID not found in claims");

            return int.Parse(value);
        }
    }
}