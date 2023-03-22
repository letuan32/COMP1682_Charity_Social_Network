using Microsoft.EntityFrameworkCore;
using TDonation.Entities;
using TDonation.Infracstructure;
using TDonation.Services.Interfaces;

namespace TDonation.Services;

public class DonationService : IDonationService
{
    private readonly DonationDbContext _dbContext;

    public DonationService(DonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateTransactionAsync(DonationTransactionEntity entity)
    {
        await _dbContext.DonationTransactionEntities.AddAsync(entity);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateTransactionStatusAsync(string internalTransactionId, TransactionStatusEnum statusEnum)
    {
        var transaction =
            await _dbContext.DonationTransactionEntities.FirstOrDefaultAsync(_ =>
                _.InternalTransactionId == internalTransactionId);

        if (transaction is null) return false;
        transaction.Status = statusEnum;
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public Task<dynamic> GetTransactionByIdAsync()
    {
        throw new NotImplementedException();
    }

    public string GenerateInternalTransactionId()
    {
        return DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid().ToString("N");
    }
}