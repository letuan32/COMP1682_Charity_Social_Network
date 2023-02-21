using System.Security.Claims;

namespace TCharity.Post.Heplers;

public class UserPropertyService
{
    private readonly IHttpContextAccessor _context;

    public UserPropertyService(IHttpContextAccessor context)
    {
        _context = context;
    }
    
    public string? GetNameIdentifier()
    {
        return _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}