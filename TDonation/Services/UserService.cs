using TDonation.Services.Interfaces;

namespace TDonation.Services;

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

    public async Task<string> GetUserId()
    {
        return _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value ?? "Anonymous";
    }

    public Task<string?> GetUserEmail()
    {
        throw new NotImplementedException();
    }
}