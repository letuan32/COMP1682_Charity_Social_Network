using FirebaseAdmin.Auth;
using TPostService.ViewModels;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedModels.Enums;
using SharedModels.Options;

namespace TPostService.Services;

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

    public async Task<bool> CreatePostRealTimeRecordAsync(int postId)
    {
        var firebaseClient = await GetPostDbFirebaseClientAsync();
        var newPostApproval = new PostRealTimeApproveStatus(postId, PostApproveStatusEnum.Pending);
        await firebaseClient
            .Child("post-approval")
            .Child(newPostApproval.Id.ToString())
            .PutAsync(newPostApproval);
        return true;
    }

    public async Task<FirebaseClient> GetPostDbFirebaseClientAsync()
    {
        return new FirebaseClient(_firebaseOption.RealTimePostDbUrl);
    }
}
