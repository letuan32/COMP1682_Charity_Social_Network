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
    public async Task<IActionResult> GetPosts()
    {
        var response = await _client.GetPostsAsync(new GetPostsRequest());
        return Ok(response);
    }
    
}