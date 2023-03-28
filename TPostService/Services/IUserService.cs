namespace TPostService.Services;

public interface IUserService
{
    Task<string?> AcquireToken();
    string GetUserId();
    Task<string?> GetUserEmail();
}