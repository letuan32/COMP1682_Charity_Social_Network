using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using T_PostService;


namespace APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostGrpc.PostGrpcClient _client;
    private readonly ILogger<WeatherController> _logger;

    public PostController(ILogger<WeatherController> logger, PostGrpc.PostGrpcClient client)
    {
        _logger = logger;
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _client.GetPostByIdAsync(new GetPostByIdRequest(){Id = id});
        return Ok(response);
    }
    
}