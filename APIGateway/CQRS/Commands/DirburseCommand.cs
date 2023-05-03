using APIGateway.DTOs.Donations;
using APIGateway.Enums;
using MediatR;
using TDonation;

namespace APIGateway.CQRS.Commands;

public class DisburseCommand : IRequest<DisburseDonationReply>
{
    public int PostId { get; set; }
    public string UserEmail { get; set; }
}