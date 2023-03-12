using TDonation.CQRS.Commands;
using TDonation.CQRS.ViewModels;
using TDonation.Services.DTOs.ZaloPay;

namespace TDonation.Services.Interfaces;

public interface IZaloPayService
{
    Task<CreateTransactionResponse> CreateTransactionAsync(CreateTransactionCommand request);
}