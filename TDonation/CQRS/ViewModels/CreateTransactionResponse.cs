namespace TDonation.CQRS.ViewModels;

public class CreateTransactionResponse
{
    public CreateTransactionResponse(string? paymentGatewayUrl, string? transactionToken, string? message)
    {
        PaymentGatewayUrl = paymentGatewayUrl;
        TransactionToken = transactionToken;
        Message = message;
    }

    public string? PaymentGatewayUrl { get; set; }
    public string? TransactionToken { get; set; }
    public string? Message { get; set; }
}