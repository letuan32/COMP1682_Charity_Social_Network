using TDonation.Entities;
using TDonation.Enums;

namespace TDonation.Services.Interfaces;

public interface IDonationService
{
    Task<bool> CreateTransactionAsync(DonationTransactionEntity entity);
    Task<bool> UpdateTransactionStatusAsync(string postId, TransactionStatusEnum statusEnum);
}