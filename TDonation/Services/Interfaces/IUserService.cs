namespace TDonation.Services.Interfaces;

public interface IUserService
{
    Task<string?> AcquireToken();
    string GetUserId();
    Task<string?> GetUserEmail();
}