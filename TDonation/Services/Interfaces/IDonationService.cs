using TDonation.Entities;

namespace TDonation.Services.Interfaces;

public interface IDonationService
{
    Task<bool> CreateTransactionAsync(DonationTransactionEntity entity);
    Task<bool> UpdateTransactionStatusAsync(string postId, TransactionStatusEnum statusEnum);
    Task<dynamic> GetTransactionByIdAsync();

    string GenerateInternalTransactionId();


}