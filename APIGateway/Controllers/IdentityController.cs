using System;
using System.Security.Claims;
using System.Threading.Tasks;
using APIGateway.CQRS.Queries;
using FirebaseAdmin.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator"></param>
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register an account
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("auth/register")]
    public async Task<IActionResult> Register(Regis regis)
    {
        UserRecordArgs args = new UserRecordArgs()
        {
            Email = regis.Email,
            EmailVerified = false,
            PhoneNumber = regis.PhoneNumber,
            Password = regis.Password,
            DisplayName = regis.Name,
            Disabled = false,
            PhotoUrl = $"https://i.pravatar.cc/150?u={regis.Email}",
        };
        
        var claims = new Dictionary<string, object>()
        {
            { ClaimTypes.Role, "user" },
        };
        UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims );

        return Ok(userRecord);
    }
    
    
    /// <summary>
    /// Create an account
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Authorize(Roles = "admin")]
    [Route("account")]
    public async Task<IActionResult> CreateAccount(Regis regis)
    {
        UserRecordArgs args = new UserRecordArgs()
        {
            Email = regis.Email,
            EmailVerified = false,
            PhoneNumber = regis.PhoneNumber,
            Password = regis.Password,
            DisplayName = regis.Name,
            Disabled = false,
            PhotoUrl = $"https://i.pravatar.cc/150?u={regis.Email}",
        };
        
        var claims = new Dictionary<string, object>()
        {
            { ClaimTypes.Role, "manager" },
        };
        UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims );
        return Ok(userRecord);
    }
    
    /// <summary>
    /// Active/InActive an account
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPatch]
    [Route("account")]
    public Task<IActionResult> ActiveAccount(string userId,bool isDisable)
    {
        // TODO: Define request body, forward the request to Identity service
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Active/InActive an account
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("auth/login")]
    public Task<IActionResult> Login()
    {
        // TODO: Define request body, forward the request to Identity service
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Get refresh token
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("auth/refesh-token")]
    public Task<IActionResult> RefeshToken()
    {
        // TODO: Define request body, forward the request to Identity service
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("user")]
    public async Task<IActionResult> GetUser([FromQuery] GetUserByEmailQuery query)
    {
        var user = await _mediator.Send(query);
        return Ok(user);
    }

}

public class Regis
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Location { get; set; }
    public string? PhoneNumber { get; set; }


}