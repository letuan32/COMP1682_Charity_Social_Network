using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace APIGateway.Services;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<string?> AcquireToken()
    {
        // var json = JsonConvert.SerializeObject(_contextAccessor.HttpContext);
        var a = _contextAccessor.HttpContext?.Request?.Headers["Authorization"];
        return a;
    }
}