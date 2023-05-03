using System.Security.Claims;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
   
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<string> GetUser()
    {
        var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        return user.Uid;
    }
    
}