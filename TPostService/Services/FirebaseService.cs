using FirebaseAdmin.Auth;

namespace TPostService.Services;

public class FirebaseService : IFirebaseService
{
    private readonly FirebaseAuth _auth;

    public FirebaseService()
    {
        _auth = FirebaseAuth.DefaultInstance;
    }

    public async Task<UserRecord> GetUserAsync(string userId)
    {
        return await _auth.GetUserAsync(userId);
    }
}
