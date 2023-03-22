namespace APIGateway.Services;

public interface IUserService
{
    Task<string?> AcquireToken();
    string GetUserIdAsync();

}