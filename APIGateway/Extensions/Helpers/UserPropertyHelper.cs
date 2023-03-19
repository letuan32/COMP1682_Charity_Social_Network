using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace APIGateway.Extensions.Helpers;

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
    
    public async Task<string?> GetToken()
    {
        const string ACCESS_TOKEN = "access_token";
        return await _context.HttpContext
            .GetTokenAsync(ACCESS_TOKEN) ?? null;
    }
}