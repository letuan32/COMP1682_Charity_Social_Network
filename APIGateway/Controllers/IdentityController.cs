using System;
using System.Threading.Tasks;
using APIGateway.CQRS.Queries;
using MediatR;
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
    public Task<IActionResult> Register()
    {
        // TODO: Define request body, forward the request to Identity service
        throw new NotImplementedException();
    }
    
    
    /// <summary>
    /// Create an account
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("account")]
    public Task<IActionResult> CreateAccount()
    {
        // TODO: Define request body, forward the request to Identity service
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Active/InActive an account
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPatch]
    [Route("account")]
    public Task<IActionResult> ActiveAccount()
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