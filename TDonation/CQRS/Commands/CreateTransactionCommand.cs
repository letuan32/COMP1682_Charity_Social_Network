using MediatR;
using TDonation.CQRS.ViewModels;

namespace TDonation.CQRS.Commands;

public class CreateTransactionCommand : IRequest<CreateTransactionResponse>
{
    public PaymentServiceEnum PaymentServiceEnum { get; set; }
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    // public string Item { get; set; } = String.Empty;
    public int Amount { get; set; }
    public string Description { get; set; } = String.Empty;
    public string? CallbackUrl { get; set; }
    public BankingType BankingType { get; set; }

}

public enum PaymentServiceEnum
{
    ZaloPay = 1
}

public enum BankingType
{
    ATM = 1,
    Visa = 2,
    EWallet=3
}

