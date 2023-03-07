using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    
    [HttpPost]
    public Task<IActionResult> Register()
    {
        // TODO: Define request body, forward the request to Identity service
        throw new NotImplementedException();
    }

}