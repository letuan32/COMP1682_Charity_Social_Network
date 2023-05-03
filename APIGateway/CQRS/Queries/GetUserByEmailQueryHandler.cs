using APIGateway.DTOs.Users;
using AutoMapper;
using FirebaseAdmin.Auth;
using MediatR;

namespace APIGateway.CQRS.Queries;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserResponse>
{
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<GetUserResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(request.UserEmail, cancellationToken);

        return _mapper.Map<GetUserResponse>(userRecord);
    }
}