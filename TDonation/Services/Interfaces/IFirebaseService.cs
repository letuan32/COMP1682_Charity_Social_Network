
using Firebase.Database;
using FirebaseAdmin.Auth;

namespace TDonation.Services.Interfaces;

public interface IFirebaseService
{
    Task<UserRecord> GetUserAsync(string userId);
    Task<bool> NewDonationEvent(int postId, long amount);
    Task<FirebaseClient> GetPostDbFirebaseClientAsync();
}