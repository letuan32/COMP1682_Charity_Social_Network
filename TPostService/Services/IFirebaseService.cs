using FirebaseAdmin.Auth;

namespace TPostService.Services;

public interface IFirebaseService
{
    Task<UserRecord> GetUserAsync(string userId);
}