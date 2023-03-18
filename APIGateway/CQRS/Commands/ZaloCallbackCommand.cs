using MediatR;

namespace APIGateway.CQRS.Commands;

public class ZaloCallbackCommand : IRequest<Unit>
{
    public string data { get; set; }
    public string mac { get; set; }
    public int type { get; set; }
    public Dictionary<string, object>? item { get; set; }
}