using MediatR;
using TDonation.CQRS.ViewModels;
using TDonation.Enums;

namespace TDonation.CQRS.Commands;

public class CreateTransactionCommand : IRequest<CreateTransactionResponse>
{
    public PaymentServiceEnum PaymentServiceEnum { get; set; }
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    // public string Item { get; set; } = String.Empty;
    public long Amount { get; set; }
    public string Description { get; set; } = String.Empty;
    public string? CallbackUrl { get; set; }
    public BankingTypeEnum BankingType { get; set; }
    public int PostId { get; set; }

    public string InternalTransactionId { get; } = DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid().ToString("N");
}