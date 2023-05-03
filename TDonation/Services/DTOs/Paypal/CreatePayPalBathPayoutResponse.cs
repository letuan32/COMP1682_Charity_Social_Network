namespace TDonation.Services.DTOs.Paypal;

public class CreatePayPalBathPayoutResponse
{
    public BatchHeaderResponse batch_header { get; set; }

}
public class BatchHeaderResponse
{
    public SenderBatchHeader sender_batch_header { get; set; }
    public string payout_batch_id { get; set; }
    public string batch_status { get; set; }
}

public class SenderBatchHeaderResponse
{
    public string sender_batch_id { get; set; }
    public string email_subject { get; set; }
    public string email_message { get; set; }
}