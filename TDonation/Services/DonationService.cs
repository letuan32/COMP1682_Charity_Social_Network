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

    public async Task<bool> CreateTransactionAsync(DonationTransactionEntity entity)
    {
        var user = _userService.GetUserId();
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
        transaction.StatusEnum = statusEnum;
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