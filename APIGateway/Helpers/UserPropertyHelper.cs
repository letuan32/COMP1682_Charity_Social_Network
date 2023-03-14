using System.Security.Claims;

namespace APIGateway.Helpers;

public class UserPropertyHelper
{
    private readonly IHttpContextAccessor _context;

    public UserPropertyHelper(IHttpContextAccessor context)
    {
        _context = context;
    }
    
    public string? GetNameIdentifier()
    {
        return _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}