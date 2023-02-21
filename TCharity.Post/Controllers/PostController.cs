using MediatR;
using Microsoft.AspNetCore.Mvc;
using TCharity.Post.Entities;
using TCharity.Post.Infrastructure;
using TCharity.Post.Queries;
using TCharity.Post.ViewModels;

namespace TCharity.Post.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private IMediator _mediator;

    public PostController(ILogger<PostController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public IEnumerable<IEnumerable<PostViewModel>> Get()
    {
        var query = new GetPostsQuery();
        var re = _mediator.Send(query);
        throw new NotImplementedException();
    }
}