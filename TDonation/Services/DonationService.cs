using Microsoft.EntityFrameworkCore;
using TDonation.Entities;
using TDonation.Enums;
using TDonation.Infracstructure;
using TDonation.Services.Interfaces;

namespace TDonation.Services;

public class DonationService : IDonationService
{
    private readonly DonationDbContext _dbContext;
    private readonly IUserService _userService;

    public DonationService(DonationDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<bool> AnyAsync(string internalTransactionId)
    {
        return await _dbContext.DonationTransactionEntities.AnyAsync(d =>
            d.InternalTransactionId == internalTransactionId);
    }

    public async Task<bool> CreateTransactionAsync(DonationTransactionEntity entity)
    {
        await _dbContext.DonationTransactionEntities.AddAsync(entity);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public Task<DonationTransactionEntity?> GetPaypalTransactionByInvoiceIdAsync(string invoiceId)
    {
        return _dbContext.DonationTransactionEntities.FirstOrDefaultAsync(d => d.InternalTransactionId == invoiceId);
    }

    public async Task<bool> UpdateTransactionStatusAsync(string internalTransactionId, TransactionStatusEnum statusEnum)
    {
        var transaction =
            await _dbContext.DonationTransactionEntities.FirstOrDefaultAsync(_ =>
                _.InternalTransactionId == internalTransactionId);

        if (transaction is null) return false;
        transaction.StatusEnum = statusEnum;
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpsertTransactionEntityByExternalIdAsync(string externalId, DonationTransactionEntity entity)
    {
        var existedEntity =
            await _dbContext.DonationTransactionEntities.AsNoTracking().FirstOrDefaultAsync(d => d.InternalTransactionId == externalId);
        if (existedEntity == null)
        {
            return await CreateTransactionAsync(entity);
        }
        _dbContext.Entry(existedEntity).CurrentValues.SetValues(entity);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public Task<long> GetDonationAmountByPostId(int postId)
    {
        return _dbContext.DonationTransactionEntities
            .Where(d => d.PostId == postId)
            .SumAsync(d => d.Amount);
    }


    public string GenerateInternalTransactionId()
    {
        return DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid().ToString("N");
    }
}