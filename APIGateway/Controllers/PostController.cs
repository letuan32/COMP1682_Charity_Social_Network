

using APIGateway.CQRS.Commands.PostCommands;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Post;
using TPostService;

namespace APIGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostGrpc.PostGrpcClient _client;
    private readonly ILogger<PostController> _logger;
    private readonly IMapper _mapper;
    private readonly IBus _bus;


    public PostController(ILogger<PostController> logger, PostGrpc.PostGrpcClient client, IMapper mapper, IBus bus)
    {
        _logger = logger;
        _client = client;
        _mapper = mapper;
        _bus = bus;
    }

    /// <summary>
    /// Retrieve a list of posts
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetPostsAsync()
    {
        var response = await _client.GetPostsAsync(new GetPostsRequest());
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieve a list of un-approve posts
    /// </summary>
    /// <returns></returns>
    [HttpGet("un-approve")]
    public async Task<IActionResult> GetPrivatePostsAsync()
    {
        var response = await _client.GetUnApprovePostsAsync(new GetUnApprovePostsRequest());
        return Ok(response);
    }
    
    /// <summary>
    /// Submit a new post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreatePostsAsync(CreatePostCommand request)
    {
        _logger.LogInformation("Start sending gRPC request to create post. Request: {@request}", request);
        var response = await _client.CreatePostAsync(_mapper.Map<CreatePostRequest>(request));
        if(response.Success)
            return Ok(response);
        return BadRequest(response);
    }
    
    /// <summary>
    /// Retrieve a post by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPostByIdAsync([FromRoute] int id)
    {
        var response = await _client.GetPostByIdAsync(new GetPostByIdRequest(){Id = id});
        return Ok(response);
    }
    
    /// <summary>
    /// Get post banking description template for donation
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("bankingDescription")]
    public async Task<IActionResult> GetPostBankingDescription([FromQuery]int postId)
    {
        var response = await _client.GetPostDonationBankingDescriptionAsync(new GetDonationBankingDescriptionRequest(){PostId = postId});
        return Ok(response);
    }
    
    /// <summary>
    /// Submit verify result for a post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("approve")]
    public async Task<IActionResult> Approve([FromBody] UpdatePostApproveStatusMessage request)
    {
        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/post-approve-status"));
        var headers = new Dictionary<string, object>();
        headers["Authorization"] = "Bearer myAccessToken";
        await endpoint.Send(request, context =>
        {
            foreach (var keyValuePair in headers)
            {
                context.Headers.Set(keyValuePair.Key, keyValuePair.Value);
            }
        });

        await _bus.Send(endpoint);
        return Ok();
    }
    
}