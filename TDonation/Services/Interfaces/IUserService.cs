namespace TDonation.Services.Interfaces;

public interface IUserService
{
    Task<string?> AcquireToken();
    Task<string> GetUserId();
    Task<string?> GetUserEmail();
}