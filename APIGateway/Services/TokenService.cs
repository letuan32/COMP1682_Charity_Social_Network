using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace APIGateway.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<string?> AcquireToken()
    {
        // var json = JsonConvert.SerializeObject(_contextAccessor.HttpContext);
        var a = _contextAccessor.HttpContext?.Request?.Headers["Authorization"];
        return a;
    }

    public string GetUserIdAsync()
    {
        return _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value ?? "Anonymous";
    }
}