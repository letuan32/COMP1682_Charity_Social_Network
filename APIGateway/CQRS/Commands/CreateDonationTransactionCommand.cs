using APIGateway.DTOs.Donations;
using APIGateway.Enums;
using MediatR;

namespace APIGateway.CQRS.Commands;

public class CreateDonationTransactionCommand : IRequest<CreateDonationTransactionResponse>
{
    public int PostId { get; set; }
    public long Amount { get; set; }
    public BankingTypeEnum BankingType { get; set; }
    public PaymentServiceEnum PaymentService { get; set; }
}