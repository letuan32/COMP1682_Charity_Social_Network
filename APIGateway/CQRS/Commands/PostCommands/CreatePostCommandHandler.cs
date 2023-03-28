using MediatR;
using TPostService;

namespace APIGateway.CQRS.Commands.PostCommands;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
{
    private readonly PostGrpc.PostGrpcClient _postGrpcClient;

    public CreatePostCommandHandler(PostGrpc.PostGrpcClient postGrpcClient)
    {
        _postGrpcClient = postGrpcClient;
    }

    public Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}