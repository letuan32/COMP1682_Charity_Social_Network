

using APIGateway.CQRS.Commands.PostCommands;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPostService;

namespace APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostGrpc.PostGrpcClient _client;
    private readonly ILogger<PostController> _logger;
    private readonly IMapper _mapper;

    public PostController(ILogger<PostController> logger, PostGrpc.PostGrpcClient client, IMapper mapper)
    {
        _logger = logger;
        _client = client;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetPostsAsync()
    {
        var response = await _client.GetPostsAsync(new GetPostsRequest());
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePostsAsync(CreatePostCommand request)
    {
        _logger.LogInformation("Start sending gRPC request to create post. Request: {@request}", request);
        var response = await _client.CreatePostAsync(_mapper.Map<CreatePostRequest>(request));
        if(response.Success)
            return Ok(response);
        return BadRequest(response);
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