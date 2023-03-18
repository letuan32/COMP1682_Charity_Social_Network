using APIGateway.DTOs.Donations;
using MediatR;

namespace APIGateway.CQRS.Commands;

public class ZaloCallbackCommand : IRequest<HandleZaloCallbackResponse>
{
    public string Data { get; set; }
    public string Mac { get; set; }
    public int Type { get; set; }
}