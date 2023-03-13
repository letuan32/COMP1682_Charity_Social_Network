namespace TDonation.Services.Interfaces;

public interface IDonationService
{
    Task<bool> CreateTransactionAsync();
    Task<bool> UpdateTransactionResultAsync();
    Task<dynamic> GetTransactionByIdAsync();
    
    
}