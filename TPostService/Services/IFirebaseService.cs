using Firebase.Database;
using FirebaseAdmin.Auth;
using TPostService.ViewModels;

namespace TPostService.Services;

public interface IFirebaseService
{
    Task<UserRecord> GetUserAsync(string userId);
    Task<bool> CreatePostRealTimeRecordAsync(int postId);
    Task<FirebaseClient> GetPostDbFirebaseClientAsync();


}