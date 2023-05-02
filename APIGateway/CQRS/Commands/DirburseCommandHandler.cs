using APIGateway.DTOs.Donations;
using MediatR;

namespace APIGateway.CQRS.Commands;

public class DisburseCommandHandler : IRequestHandler<DisburseCommand, DisburseResponse>
{
    public Task<DisburseResponse> Handle(DisburseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}