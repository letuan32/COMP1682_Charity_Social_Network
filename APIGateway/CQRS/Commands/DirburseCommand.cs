using APIGateway.DTOs.Donations;
using APIGateway.Enums;
using MediatR;

namespace APIGateway.CQRS.Commands;

public class DisburseCommand : IRequest<DisburseResponse>
{
    public int PostId { get; set; }
    public PaymentServiceEnum PaymentService { get; set; }
}