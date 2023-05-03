using APIGateway.DTOs.Users;
using MediatR;

namespace APIGateway.CQRS.Queries;

public class GetUserByEmailQuery : IRequest<GetUserResponse>
{
    public string UserEmail { get; set; }
}