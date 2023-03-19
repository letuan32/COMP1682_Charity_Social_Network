namespace TPostService.Services;

public interface IUserService
{
    Task<string?> AcquireToken();
    Task<string> GetUserId();
    Task<string?> GetUserEmail();
}