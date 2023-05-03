

using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using SharedModels.Enums;
using SharedModels.Options;
using TDonation.Services.Interfaces;

namespace TDonation.Services;

public class FirebaseService : IFirebaseService
{
    private readonly FirebaseAuth _auth;
    private readonly ShareFirebaseOption _firebaseOption;


    public FirebaseService(IOptions<ShareFirebaseOption> firebaseOption)
    {
        _auth = FirebaseAuth.DefaultInstance;
        _firebaseOption = firebaseOption.Value;

    }

    public async Task<UserRecord> GetUserAsync(string userId)
    {
        return await _auth.GetUserAsync(userId);
    }

    public async Task<bool> NewDonationEvent(int postId, long amount)
    {
        var firebaseClient = await GetPostDbFirebaseClientAsync();
        await firebaseClient
            .Child("post-approval")
            .Child(postId.ToString())
            .Child("Amount")
            .PutAsync(amount);
        return true;

    }
    

    public async Task<FirebaseClient> GetPostDbFirebaseClientAsync()
    {
        return new FirebaseClient(_firebaseOption.RealTimePostDbUrl);
    }
}
