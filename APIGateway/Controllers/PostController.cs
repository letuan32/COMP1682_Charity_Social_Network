

using Microsoft.AspNetCore.Mvc;
using TPostService;

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
    public async Task<IActionResult> GetPostsAsync()
    {
        var response = await _client.GetPostsAsync(new GetPostsRequest());
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPostByIdAsync([FromRoute] int id)
    {
        var response = await _client.GetPostByIdAsync(new GetPostByIdRequest(){Id = id});
        return Ok(response);
    }
    
    [HttpGet]
    [Route("bankingDescription")]
    public async Task<IActionResult> GetPostBankingDescription([FromQuery]int postId)
    {
        var response = await _client.GetPostDonationBankingDescriptionAsync(new GetDonationBankingDescriptionRequest(){PostId = postId});
        return Ok(response);
    }
}