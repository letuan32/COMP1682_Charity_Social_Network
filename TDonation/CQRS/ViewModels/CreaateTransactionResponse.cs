namespace TDonation.CQRS.ViewModels;

public class CreateTransactionResponse
{
    public string? PaymentGatewayUrl { get; set; }
    public string? TransactionToken { get; set; }
    public string? Message { get; set; }
}