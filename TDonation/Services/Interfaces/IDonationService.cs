using TDonation.Entities;
using TDonation.Enums;

namespace TDonation.Services.Interfaces;

public interface IDonationService
{
    Task<bool> AnyAsync(string internalTransactionId);
    Task<bool> CreateTransactionAsync(DonationTransactionEntity entity);
    Task<bool> UpdateTransactionStatusAsync(string postId, TransactionStatusEnum statusEnum);
    Task<bool> UpsertTransactionEntityByExternalIdAsync(string externalId, DonationTransactionEntity entity);
    Task<long> GetDonationAmountByPostId(int postId);


}