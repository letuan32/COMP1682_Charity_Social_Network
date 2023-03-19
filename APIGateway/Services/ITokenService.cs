namespace APIGateway.Services;

public interface ITokenService
{
    Task<string?> AcquireToken();
}