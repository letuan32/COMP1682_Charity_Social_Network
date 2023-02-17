using System.Security.Claims;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
   
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "create")]
    public async Task<string> CreateUser([FromBody] Regis regis)
    {
        UserRecordArgs args = new UserRecordArgs()
        {
            Email = regis.Email,
            EmailVerified = false,
            PhoneNumber = regis.PhoneNumber,
            Password = regis.Password,
            DisplayName = regis.Name,
            Disabled = false,
        };
        
        var claims = new Dictionary<string, object>()
        {
            { ClaimTypes.Role, regis.Role },
        };
        UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims );

        return userRecord.Uid;
    }
    
}