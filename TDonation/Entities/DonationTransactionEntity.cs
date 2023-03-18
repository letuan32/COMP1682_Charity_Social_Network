using TDonation.CQRS.Commands;

namespace TDonation.Entities;

public class DonationTransactionEntity : BaseIdEntity<int>, IBaseEntity
{
    public DonationTransactionEntity(int postId, long amount, string internalSenderId, string internalReceiverId, string internalTransactionId, TransactionTypeEnum transactionType, CurrencyEnum currency, PaymentServiceEnum paymentServiceEnum, string description, TransactionStatusEnum status, string? message, string transactionToken)
    {
        PostId = postId;
        Amount = amount;
        InternalSenderId = internalSenderId;
        InternalReceiverId = internalReceiverId;
        InternalTransactionId = internalTransactionId;
        TransactionType = transactionType;
        Currency = currency;
        PaymentServiceEnum = paymentServiceEnum;
        Description = description;
        Status = status;
        Message = message;
        TransactionToken = transactionToken;
    }

    
    public int PostId { get; set; }
    public long Amount { get; set; }
    public string InternalSenderId { get; set; }
    public string InternalReceiverId { get; set; }
    public string InternalTransactionId { get; set; }
    public TransactionTypeEnum TransactionType { get; set; }
    public CurrencyEnum Currency { get; set; }
    public PaymentServiceEnum PaymentServiceEnum { get; set; }
    public string TransactionToken { get; set; }
    public string Description { get; set; }
    public TransactionStatusEnum Status { get; set; }
    public string? Message { get; set; }
    public string? AdditionalData { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}



public enum TransactionTypeEnum
{
    Donation = 1,
    Disbursement = 2
}

public enum CurrencyEnum
{
    VND = 1,
    USD = 2
}

public enum TransactionStatusEnum
{
    InProcess = 1,
    Done = 2,
    Failed = 3
}