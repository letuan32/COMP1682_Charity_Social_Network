using TDonation.CQRS.Commands;

namespace TDonation.Entities;

public class DonationTransactionEntity : BaseIdEntity<int>, IBaseEntity
{
    public int PostId { get; set; }
    public PaymentServiceEnum PaymentServiceEnum { get; set; }
    public string Description { get; set; }
    public string ExternalTransactionId { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}