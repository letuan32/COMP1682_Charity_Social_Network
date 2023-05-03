namespace TDonation.Services.DTOs.Paypal;

public class CreatePayPalBathPayoutRequest
{
    public List<PayPalPayoutItem> items { get; set; }
    public SenderBatchHeader sender_batch_header { get; set; }
}

public class Amount
{
    public string currency { get; set; }
    public string value { get; set; }
}

public class PayPalPayoutItem
{
    public string receiver { get; set; }
    public Amount amount { get; set; }
    public string recipient_type { get; set; }
    public string note { get; set; }
    public string sender_item_id { get; set; }
    public string recipient_wallet { get; set; }
}


public class SenderBatchHeader
{
    public string sender_batch_id { get; set; }
    public string email_subject { get; set; }
    public string email_message { get; set; }
}